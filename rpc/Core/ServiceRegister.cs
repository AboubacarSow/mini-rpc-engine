using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpc.Core;

internal class ServiceRegister
{
    public Dictionary<string, object> Services { get; set; }

    public void Register(string  serviceName, object service)
    {
        Services[serviceName] = service;    
    }

    public object Get(string serviceName)
    {
        return Services[serviceName];
    }


}
