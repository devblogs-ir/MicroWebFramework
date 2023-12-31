using System.Net;
namespace PipelineDesignPattern;

public class HttpContext
{
    public required string IP { get; set; }

    public string Url { get; set; } = null!;

    public HttpListenerRequest Request { get; set; } = null!;

    public HttpListenerResponse Response { get; set; } = null!;

    public CountryIPAddress Country => Enum.TryParse(IP[^2..], out CountryIPAddress country) ? country : 0;

}
public enum CountryIPAddress
{
    UnKnown = 0,
    Iran = 10,
    China = 20,
    USA = 30,
}