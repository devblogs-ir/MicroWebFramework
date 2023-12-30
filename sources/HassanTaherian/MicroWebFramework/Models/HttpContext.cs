namespace PipelineDesignPattern;
public class HttpContext
{
    public required string IpAdrress { get; init; }
    public required int Id { get; init; }
    public required HttpRequest Request { get; set; }

}
