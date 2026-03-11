using engine.Core;
using engine.Protocol;
using System.Reflection;

namespace engine.Wrapper;

public class RpcProxyFactory : DispatchProxy
{
    private  RpcClient _client;
    private  string _serviceName;
    protected override  object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        var methodName = targetMethod?.Name;
        var parameters = args;
        if (methodName == null) 
            throw new ArgumentNullException(nameof(methodName));
        var request = new RpcRequest(_serviceName,methodName, parameters!);
        var response = _client.CallAsync(request).GetAwaiter().GetResult() ;
        return response;
    }

    public static T Create<T>(RpcClient client) where T : class
    {
        if (!typeof(T).IsInterface)
            throw new ArgumentException("T must be an Interface");
        object proxy = DispatchProxy.Create<T, RpcProxyFactory>();
       ((RpcProxyFactory)proxy)._client = client;
        var serviceName = typeof(T).Name;
        if (serviceName.StartsWith("I"))
        {
            serviceName = serviceName.Substring(1,serviceName.Length-1);
        }
        ((RpcProxyFactory)proxy)._serviceName = serviceName;
        return (T)proxy;
    }

    
}