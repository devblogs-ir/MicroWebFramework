using MicroFramwork.Exceptions;
using MicroFramwork.Model;

namespace MicroFramwork.Controllers;

public class UserController : BaseController
{
    private readonly List<User> _users = new()
    {
        new("javid", "hassani"),
        new("reza", "ahmadi"),
        new("navid", "rezaie"),
        new("arman", "mohammadi"),
    };

    public IReadOnlyList<User> GetAll()
    {
        return _users;
    }

    public User GetUserById(int id)
    {
        var user = _users[id];
        if (user is null)
            throw new NotFoundException("user notfound");

        return user;
    }
}
