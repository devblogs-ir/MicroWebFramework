namespace MicroWebFrameworkConsole.Exceptions
{
    public class GeoLocationException:Exception
    {
        public GeoLocationException(string locationIP):base(string.Format("Service to {0} is resticted", locationIP))
        {
            
        }
    }
}
