namespace HttpSelfHostConsole.Framework.Models
{
    public class RoutingContext
    {
        public string controllerName { get; set; }
        public string actionName { get; set; }
        public int? parameterId { get; set; }
    }
}
