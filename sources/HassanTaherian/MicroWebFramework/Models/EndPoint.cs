namespace PipelineDesignPattern;
public record EndPoint
{
    public required string ControllerName { get; set; }
    public required string ActionName { get; set; }
    public string? Parameter { get; set; }
}
