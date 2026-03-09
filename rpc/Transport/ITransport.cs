using rpc.Protocol.Models;

namespace rpc.Transport;

public interface ITransport
{
    Task SendAsync(byte [] data);
    Task<byte[]> ReceiveAsync();
}