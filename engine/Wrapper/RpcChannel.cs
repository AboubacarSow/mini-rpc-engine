using engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace engine.Wrapper;

public class RpcChannel
{
    public static RpcClient ForAddress(int port)
    {
        return new RpcClient(new TcpClient("localhost", port));
    }
}
