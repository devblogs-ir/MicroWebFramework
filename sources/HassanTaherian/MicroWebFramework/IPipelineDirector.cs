namespace PipelineDesignPattern;
public interface IPipelineDirector
{
    void Process(HttpContext httpContext);
}
