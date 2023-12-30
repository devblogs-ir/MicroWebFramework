using Dumpify;

namespace PipelineDesignPattern;
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

        if (_ipService.IsOriginFromBannedCountries(context.IpAdrress))
        {
            var originCountry = _ipService.GetOriginCountry(context.IpAdrress);
            throw new AccessingFromBannedCountryException(originCountry?.Name);
        }

        next?.Invoke(context);
    }
}
