namespace MicroWebFramework;
public class HttpContext
{
    public required string IpAdrress { get; init; }
    public required Guid Id { get; init; }
    public required HttpRequest Request { get; init; }
    public required HttpResponse Response { get; init; }
}
