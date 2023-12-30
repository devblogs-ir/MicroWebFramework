using MicroWebFramework.Middlewares;

namespace MicroWebFramework.Builder;
//
// Summary:
//     ExceptionHanding extension methods for MicroWebFramework.Builder.PipelineBuilder.
public static class ExceptionHandingPipelineBuilderExtensions
{
    //
    // Summary:
    //     Adds the ExceptionHanding middleware to the pipeline.
    //
    // Parameters:
    //   builder:
    //     The MicroWebFramework.Builder.PipelineBuilder.
    //
    // Returns:
    //     The pipeline builder.
    public static PipelineBuilder UseExceptionHanding(this PipelineBuilder builder)
    {
        return builder.Add<ExceptionHandlingMiddleware>();
    }
}