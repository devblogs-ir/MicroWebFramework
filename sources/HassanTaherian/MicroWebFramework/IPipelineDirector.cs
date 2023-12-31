namespace MicroWebFramework;
public interface IPipelineDirector
{
    void Process(HttpContext httpContext);
}
