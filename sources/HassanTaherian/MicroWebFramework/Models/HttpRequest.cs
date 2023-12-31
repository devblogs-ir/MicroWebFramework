namespace MicroWebFramework;
public class HttpRequest
{
    public required string Url { get; init; }
    public EndPoint? EndPoint { get; set; }
}
