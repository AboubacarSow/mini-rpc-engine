using Microsoft.Extensions.Logging;
using engine.Marshaller.Models;
using engine.Protocol;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Configuration;

namespace engine.Transport;



public class TransportListener(ILogger<TransportListener> logger) :IDisposable
{
    internal readonly TcpListener _listener = new(IPAddress.Any,50000);
    private readonly ILogger<TransportListener> _logger = logger;
    private int? Port { get; set; }
    public void Start()
    {
        _listener.Start();
        _logger.LogInformation(message: $"Server RPC Listening on Port:{50000}");
    }
    public async Task<TransportChannel> AcceptConnectionAsync()
    {
         var client = await _listener.AcceptTcpClientAsync();
        _logger.LogInformation("New client connected!");
        return new TransportChannel(client.GetStream());
    }

    public void Dispose()
    {
        _listener?.Stop();
    }

    public int? GetPort()
    {
        return Port;
    }
}
