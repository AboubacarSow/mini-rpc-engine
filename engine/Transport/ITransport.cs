

namespace engine.Transport;

public interface ITransport
{
    Task SendAsync(byte [] data, CancellationToken cancellationToken);
    Task<byte[]> ReceiveAsync(CancellationToken cancellationToken);
}