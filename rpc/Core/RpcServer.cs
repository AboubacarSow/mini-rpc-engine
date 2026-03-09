using rpc.Core.Interfaces;
using rpc.Marshaller.Models;
using rpc.Protocol;
using rpc.Transport;

namespace rpc.Core;

public class RpcServer(TransportListener transport)
{
    private readonly TransportListener _transportListener = transport;
    private readonly JsonMarshaller _marshaller= new();
    private readonly IRpcRequestHandler _rpcHandler;

    public async Task StartAsync()
    {
         _transportListener.Start();

        while (true)
        {
            var channel = _transportListener.AcceptConnection();

            _ = HandleConnectionAsync(channel);
        }
    }
    public async Task HandleConnectionAsync(TransportChannel channel)
    {
        while (true)
        {
            var data = await channel.ReceiveAsync();
            var request = _marshaller.UnMarshal<RpcRequest>(data);
            var response = await _rpcHandler.Handler(request);
            var bytes = _marshaller.Marshal(response);
            await channel.SendAsync(bytes);
        }
    }

   
}
