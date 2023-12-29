using Dumpify;
using MicroWebFramework.Presentation.CustomExceptions;
using MicroWebFramework.Presentation.Pipeline;

namespace MicroWebFramework.Presentation.Framework;
public class ExceptionHandlingPipe : Pipe
{
    public ExceptionHandlingPipe(Action<PipelineContext> next) : base(next)
    {
    }
    public override void Invoke(PipelineContext context)
    {
        try
        {
            "starting exception handling".Dump();
            Next(context);
        }
        catch (InvalidIpAddressException ex) { "Invalid ip address ".Dump(ex.Message); }
        catch (InaccessibilityException ex) { "Invalid request from ".Dump(ex.Message); }
        catch (UnknownIpAddressException ex) { "Ip address is unknown ".Dump(ex.Message); }
        catch (InvalidRequestException ex) { "Url is not valid ".Dump(ex.Message); }
        catch (NotImplementedPipelineException) { "No pipes have been added to the pipeline".Dump(); }
        catch (Exception ex) { "exception occurod".Dump(ex.Message); }
    }
}
