using MicroWebFramework.Middlewares;

namespace MicroWebFramework.Builder;
//
// Summary:
//     Authentication extension methods for MicroWebFramework.Builder.PipelineBuilder.
public static class AuthenticationPipelineBuilderExtensions
{
    //
    // Summary:
    //     Adds the Authentication middleware to the pipeline.
    //
    // Parameters:
    //   builder:
    //     The MicroWebFramework.Builder.PipelineBuilder.
    //
    // Returns:
    //     The pipeline builder.
    public static PipelineBuilder UseAuthentication(this PipelineBuilder builder)
    {
        return builder.Add<AuthenticationMiddleware>();
    }
}