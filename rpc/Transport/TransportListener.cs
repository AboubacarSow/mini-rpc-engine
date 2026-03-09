using Microsoft.Extensions.Logging;
using rpc.Marshaller.Models;
using rpc.Protocol;
using System.Net;
using System.Net.Sockets;

namespace rpc.Transport;



public class TransportListener(ILogger<TransportListener> logger)
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
    public TransportChannel AcceptConnection()
    {
         var client = _listener.AcceptTcpClient();
        _logger.LogInformation("New client connected!");
        return new TransportChannel(client.GetStream());
    }
}
