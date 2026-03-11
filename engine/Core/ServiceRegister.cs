using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace engine.Core;

public class ServiceRegister
{
    public Dictionary<string, object> _services { get; set; } = new();

    public void AddService(string  serviceName, object service)
    {
        _services[serviceName] = service;    
    }

    public object Get(string serviceName)
    {
        return _services[serviceName];
    }


}
