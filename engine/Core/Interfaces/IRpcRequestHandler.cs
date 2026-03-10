using engine.Protocol;

namespace engine.Core.Interfaces;

public interface IRpcRequestHandler
{
    public Task<RpcResponse> Handler(RpcRequest request);
}