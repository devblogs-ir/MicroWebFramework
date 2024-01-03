using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFrameWork.Pipes;

public class AuthenticationPipe : BasePipe
{
    public AuthenticationPipe(Action<HttpContext> next) : base(next)
    {
            
    }

    public override void HandlePipe(HttpContext context)
    {
        if (string.IsNullOrWhiteSpace(context.IpAddress))
            throw new CustomException("Ip can't be null");

        "Start authenticate user".Dump();

        if (context.IpAddress.Contains("Iran"))
        {
            throw new CustomException("Iranian IPs don't have access to the program");
        }

        if (_next is not null) _next(context);

        "End of authenticate user".Dump();
    }
}
