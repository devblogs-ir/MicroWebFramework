using Dumpify;
using MicroWebFrameworkConsole.Exceptions;

namespace MicroWebFrameworkConsole.Pipes
{
    public class LocationManagmentPipe : Pipe
    {
        public LocationManagmentPipe()
        {
            _next = null!;
        }
        public LocationManagmentPipe(Func<HTTPContext, string> next) : base(next)
        {

        }

        public override string Handle(HTTPContext context)
        {
            //"considering IP Address...".Dump();
            try
            {
                if (context.IP is "iran")
                {
                    throw new GeoLocationException(context.IP);
                }
                else
                {
                    if (_next is not null)
                        return _next(context);
                    else
                        return "";
                }
            }
            catch (GeoLocationException ex)
            {
                return ex.Message;
            }
        }
    }
}
