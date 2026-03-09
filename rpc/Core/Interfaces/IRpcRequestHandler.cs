using rpc.Protocol;

namespace rpc.Core.Interfaces;

public interface IRpcRequestHandler
{
    public Task<RpcResponse> Handler(RpcRequest request);
}