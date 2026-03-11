using engine.Core.Interfaces;
using engine.Marshaller.Models;
using engine.Protocol;
using engine.Transport;
using System.Net.Sockets;
using System.Threading.Channels;

namespace engine.Core;

public class RpcClient(TcpClient client): IRpcClient
{
    private readonly JsonMarshaller _marshaller = new();
    public async Task<RpcResponse> CallAsync(string serviceName, string method,
        CancellationToken cancellationToken = default, params object[] parameters)
    {
        var _channel = new TransportChannel(client.GetStream());
        RpcRequest request = new(serviceName, method, parameters);
        var data = _marshaller.Marshal(request);

        await _channel.SendAsync(data,cancellationToken);

        var responseBytes = await _channel.ReceiveAsync(cancellationToken);
        return _marshaller.UnMarshal<RpcResponse>(responseBytes);
    }

 
}