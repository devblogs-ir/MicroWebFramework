using MicroWebFramework.Models;

namespace MicroWebFramework;

public class UserController
{
    private readonly HttpContext _httpContext;
    private List<User> _users;

    public UserController(HttpContext httpContext)
    {
        _httpContext = httpContext;
        _users = new List<User>
        {
            new User { Id = 1, Name = "User1" },
            new User { Id = 2, Name = "User2" },
            new User { Id = 3, Name = "User3" }
        };
    }

    public string GetAllUser()
    {
        return $"Get All Users";
    }

    public User GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);
}
