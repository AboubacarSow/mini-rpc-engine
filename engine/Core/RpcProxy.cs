using engine.Protocol;
using engine.Core.Interfaces;

namespace engine.Core;

public class RpcProxy(Register register) : IRpcRequestHandler
{
    public Task<RpcResponse> Handler(RpcRequest request)
    {
        var service = register.Get(request.ServiceName)
            ?? throw new ArgumentNullException($"Service:{request.ServiceName} is not registered");
        var methodInfo = service.GetType().GetMethod(request.Method)
            ?? throw new InvalidOperationException($"Method ${request.ServiceName}.{request.Method} is not found");
        return (Task<RpcResponse>)methodInfo?.Invoke(service, request.Parameters)!;
    }

}
