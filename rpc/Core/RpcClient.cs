using rpc.Core.Interfaces;
using rpc.Marshaller.Models;
using rpc.Protocol;
using rpc.Transport;
using System.Threading.Channels;

namespace rpc.Core;

public class RpcClient(TransportChannel _channel): IRpcClient
{
    private readonly JsonMarshaller _marshaller = new();
    public async Task<RpcResponse> CallAsync(string method, params object[] parameters)
    {
        RpcRequest request = new(method, parameters);
        var data = _marshaller.Marshal(request);

        await _channel.SendAsync(data);

        var responseBytes = await _channel.ReceiveAsync();

        return _marshaller.UnMarshal<RpcResponse>(responseBytes);
    }
}