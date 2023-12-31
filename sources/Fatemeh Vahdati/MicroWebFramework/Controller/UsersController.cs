using MicroWebFramework.Model;
using MicroWebFramework.Services;
using System.Net;

namespace MicroWebFramework.Controller
{
    public class UsersCcontroller
    {
        UserServices userService = new();
        private readonly HttpListenerContext _HttpListenerContext;

        public UsersCcontroller()
        {
            _HttpListenerContext = null!;
        }
        public UsersCcontroller(HttpListenerContext HttpListenerContext)
        {
            _HttpListenerContext = HttpListenerContext;
        }

        public List<User> Getall()
        {
            return userService.GetAll(); 
        }

        public User Getuserbyid(int id)
        {
            return userService.GetUserById(id);
        }
    }
}
