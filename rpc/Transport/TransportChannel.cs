using System.Net.Sockets;

namespace rpc.Transport;

public class TransportChannel(NetworkStream stream) : ITransport
{
    private readonly NetworkStream _stream = stream;
    public async Task<byte[]> ReceiveAsync()
    {
       return await FramingProtocol.ReadAsync(_stream);
    }
    public async Task SendAsync(byte [] data)
    {
        await FramingProtocol.WriteAsync(data, _stream);
    }
}
