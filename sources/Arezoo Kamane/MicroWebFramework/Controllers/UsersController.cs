
using MicroWebFramework.Models;

namespace MicroWebFramework.Controllers;

public class UsersController
{
    private readonly List<User> _users;
    private readonly HttpContext _httpContext;

    public UsersController(HttpContext context)
    {
        _httpContext = context;
        _users = new()
        {
            new User()
            {
                Id = 1,
                Username = "Arezoo",
                Email = "arezoo@example.com"
            },
            new User()
            {
                Id = 2,
                Username = "Ali",
                Email = "ali@example.com"
            }
        };
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }

    public User GetUserById(int id)
    {
        return _users.Find(t => t.Id == id);
    }


}

