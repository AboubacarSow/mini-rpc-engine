using engine.Protocol;
using engine.Core.Interfaces;

namespace engine.Core;

public class RpcProxy(ServiceRegister register) : IRpcRequestHandler
{
    public  RpcResponse Handle(RpcRequest request)
    {
        var service = register.Get(request.ServiceName)
            ?? throw new ArgumentNullException($"Service:{request.ServiceName} is not registered");
        var methodInfo = service.GetType().GetMethod(request.Method)
            ?? throw new InvalidOperationException($"Method ${request.ServiceName}.{request.Method} is not found");
        var  message = methodInfo?.Invoke(service, request.Parameters)!;

        return new RpcResponse(message);
     


    }

}
