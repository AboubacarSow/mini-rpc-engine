using Microsoft.Extensions.Logging;
using engine.Marshaller.Models;
using engine.Protocol;
using System.Net;
using System.Net.Sockets;

namespace engine.Transport;



public class TransportListener(ILogger<TransportListener> logger) :IDisposable
{
    internal readonly TcpListener _listener = new(IPAddress.Any,0);
    private readonly ILogger<TransportListener> _logger = logger;
    public void Start()
    {
        _listener.Start();
        int boundedPort = ((IPEndPoint)_listener.LocalEndpoint).Port;

        _logger.LogInformation(message: "Server RPC Listening on Port:[@boundedPort]",
                               boundedPort);
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
}
