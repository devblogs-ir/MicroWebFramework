namespace MicroWebFramework;
public interface IUiAdapter
{
    Task<HttpContext?> GetRequestAsync();
    void SendResponse(HttpContext context);
}
