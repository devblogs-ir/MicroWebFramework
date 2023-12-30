using MicroWebFramework.Middlewares;

namespace MicroWebFramework.Builder;
//
// Summary:
//     Endpoint extension methods for MicroWebFramework.Builder.PipelineBuilder.
public static class EndpointPipelineBuilderExtensions
{
    //
    // Summary:
    //     Adds the Endpoint middleware to the pipeline.
    //
    // Parameters:
    //   builder:
    //     The MicroWebFramework.Builder.PipelineBuilder.
    //
    // Returns:
    //     The pipeline builder.
    public static PipelineBuilder UseEndPoint(this PipelineBuilder builder)
    {
        return builder.Add<EndPointMiddleware>();
    }
}