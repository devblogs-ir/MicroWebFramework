using MicroWebFrameworkConsole;
using MicroWebFrameworkConsole.Pipes;
using System.Reflection;

namespace PipelineDesignPattern.Pipes
{
    public class EndPointPipe : Pipe
    {
        public EndPointPipe()
        {
            _next = null!;
        }
        public EndPointPipe(Func<HTTPContext, string> next) : base(next)
        {

        }
        public override string Handle(HTTPContext context)
        {
            //Console.WriteLine("a request recieved from " + context.IP.ToString());
            var urlParts = context.Url.Split('/');
            var controllerName = urlParts[1] + "Controller";
            var actionMethod = urlParts[2];
            string actionId = null;
            if (urlParts.Count() > 3)
            {
                actionId = urlParts[3];
            }
            //Get All Controllers in controller folder
            var controllersList = Assembly.GetExecutingAssembly().GetTypes()
                                 .Where(t => t.Namespace == "PipelineDesignPattern.Controllers");

            Type controllerType = null;
            MethodInfo method = null;

            //looking for called controller in contollers list
            try
            {
                if (controllersList.Any(t => t.Name.ToLower() == controllerName.ToLower()))
                {
                    controllerType = controllersList.FirstOrDefault(t => t.Name.ToLower() == controllerName.ToLower());
                }
                else
                {
                    throw new NotFoundUrlException();
                }

                //looking for called action in methodes list of contoller
                if (controllerType.GetMethods().Any(m => m.Name.ToLower() == actionMethod.ToLower()))
                {
                    method = controllerType.GetMethods().FirstOrDefault(m => m.Name.ToLower() == actionMethod.ToLower());
                    byte id = 0;
                    var controllerInstance = Activator.CreateInstance(controllerType, new object[] { context });

                    if (method.GetParameters().Count() is not 0)
                    {
                        return (string)method.Invoke(controllerInstance, new object[] { byte.Parse(actionId) });
                    }
                    else
                        return (string)method.Invoke(controllerInstance, new object[] {  });
                }
                else
                {
                    throw new NotFoundUrlException();
                }
            }
            catch (NotFoundUrlException ex)
            {
                return ex.Message + "Please Check Route Data";
            }
            finally
            {
                if (_next is not null)
                    _next(context);
            }

        }
    }
}
