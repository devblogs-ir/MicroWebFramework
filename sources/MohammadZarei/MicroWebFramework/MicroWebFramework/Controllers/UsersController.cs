namespace MicroWebFramework.Controllers;

public sealed class UsersController
{
    private readonly Context _context;
    private readonly List<User> _users;

    public UsersController(Context context)
    {
        _context = context;
        _users = new List<User>()
        {
            new User { FullName = "Ali Alavi" },
            new User { FullName = "Shirin Tehrani" },
            new User { FullName = "Reza Razavi" }
        };
    }

    public IReadOnlyList<User> GetAll() => _users;

    public User Get(int id) => _users[id];
}
