using System.Net.Sockets;

namespace engine.Transport;

public class TransportChannel(NetworkStream stream) : ITransport, IDisposable
{
    private readonly NetworkStream _stream = stream;

    public void Dispose()
    {
        _stream.Close();
    }

    public async Task<byte[]> ReceiveAsync(CancellationToken cancellationToken)
    {
        return cancellationToken.IsCancellationRequested ? 
            throw new OperationCanceledException() : await FramingProtocol.ReadAsync(_stream);
    }
    public async Task SendAsync(byte [] data, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await FramingProtocol.WriteAsync(data, _stream);
    }
}
