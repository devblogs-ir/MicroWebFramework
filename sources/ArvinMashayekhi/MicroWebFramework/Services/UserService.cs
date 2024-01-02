using MicroWebFramework.Common;
using MicroWebFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Services;
public class UserService(HttpContext httpContext) : IUserService
{
    private readonly HttpContext _httpContext = httpContext;

    List<User> users = new()
    {
        new User() { Id = 1, Name = "User1" },
        new User() { Id = 2, Name = "User2" },
    };
    public void GetById(int id)
    {
        if (!users.Any(p => p.Id == id))
        {
            _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"No user was found with id: {id}!!"));
            return;
        }
        _httpContext.Response.OutputStream.Write(
        Encoding.UTF8.GetBytes(
                    users.SingleOrDefault(p => p.Id == id).Name));
    }
}

