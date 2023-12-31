using Dumpify;

namespace MicroWebFramework;
public class AuthorizationHandler : BaseHandler
{
    private readonly IIpService _ipService;
    public AuthorizationHandler(IIpService ipService)
    {
        _ipService = ipService;
    }
    public override void Handle(HttpContext context)
    {
        "Start Auth".Dump();

        if (_ipService.IsOriginFromBannedCountries(context.IpAddress))
        {
            var originCountry = _ipService.GetOriginCountry(context.IpAddress);
            throw new AccessingFromBannedCountryException(originCountry?.Name);
        }

        next?.Invoke(context);
    }
}
