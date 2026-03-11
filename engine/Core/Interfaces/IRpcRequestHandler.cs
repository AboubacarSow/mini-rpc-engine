using engine.Protocol;

namespace engine.Core.Interfaces;

public interface IRpcRequestHandler
{
    public RpcResponse Handle(RpcRequest request);
}