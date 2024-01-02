using MicroWebFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Exeptions;
public class IPExeptionHandler : Exception
{
    public IPExeptionHandler(string ip)
    : base(string.Format($"{ip} this ip address is bannd"))
    {
    }

    public IPExeptionHandler(string message, Exception Exception)
        : base(message, Exception)
    {
    }
    
}
public class ControllerExeptionHandler: Exception
{
    public ControllerExeptionHandler(): base($"controller is provided for this route.")
    {
        
    }
}
public class ActionExeptionHandler : Exception
{
    public ActionExeptionHandler(): base($"Action is provided for this route.")
    {
        
    }
}