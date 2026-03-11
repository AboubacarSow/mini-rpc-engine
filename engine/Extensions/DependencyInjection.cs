using engine.Core;
using engine.Core.Interfaces;
using engine.Marshaller.Models;
using engine.Transport;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace engine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRpcEngine(this IServiceCollection services)
    {
        services.AddSingleton<IRpcRequestHandler, RpcProxy>();

        services.AddTransient<TransportListener>();
        services.AddSingleton<JsonMarshaller>();
      
        services.AddTransient<IRpcClient, RpcClient>();
        services.AddTransient<TcpClient>();

        services.AddHostedService<RpcServer>();

        return services;
    }

    public static void AddServices(this IServiceCollection services,Action<ServiceRegister> serviceRegister)
    {
   
        var register = new ServiceRegister();

        serviceRegister(register);

        services.AddSingleton(register);
    }
}
