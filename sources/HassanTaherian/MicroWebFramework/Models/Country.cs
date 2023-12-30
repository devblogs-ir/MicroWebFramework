namespace PipelineDesignPattern;

public record Country
{
    public required string Name { get; init; }
    public required int Code { get; init; }
    public required bool IsBanned { get; init; }
}