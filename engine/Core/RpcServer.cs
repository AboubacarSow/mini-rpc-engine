using System.ComponentModel;
using System.Runtime.InteropServices;
using engine.Core.Interfaces;
using engine.Marshaller.Models;
using engine.Protocol;
using engine.Transport;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace engine.Core;

public class RpcServer(TransportListener transport,ILogger<RpcServer> logger,
JsonMarshaller marshaller) : BackgroundService
{
    private readonly TransportListener _transportListener = transport;
    private readonly JsonMarshaller _marshaller= marshaller;
    private readonly IRpcRequestHandler _rpcProxy;
    private readonly ILogger<RpcServer> _logger= logger;


    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
         _transportListener.Start();
        try
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var channel =await _transportListener.AcceptConnectionAsync();

                _ = HandleConnectionAsync(channel, stoppingToken);
                
            }
        }
        catch(Exception exception) when (exception is OperationCanceledException)
        {
            _logger.LogWarning($"Something went wrong:{exception.Message}. Server is shooting down");
        }
        finally
        {
            _transportListener.Dispose();
        }
    }
    private async Task HandleConnectionAsync(TransportChannel channel,CancellationToken cancellationToken)
    {
        using (channel)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var data = await channel.ReceiveAsync(cancellationToken);
                    if(data == null) break;
                    var request = _marshaller.UnMarshal<RpcRequest>(data);
                    var response = await _rpcProxy.Handler(request);
                    var bytes = _marshaller.Marshal(response);
                    await channel.SendAsync(bytes, cancellationToken);
                    
                }
            }
            catch(Exception ex)
            {
                channel.Dispose();
            }
        }
            
        
    }
}
