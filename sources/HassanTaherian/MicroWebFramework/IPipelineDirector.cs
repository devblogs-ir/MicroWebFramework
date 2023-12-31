namespace MicroWebFramework;
public interface IPipelineDirector
{
    HttpContext Process(HttpContext httpContext);
}
