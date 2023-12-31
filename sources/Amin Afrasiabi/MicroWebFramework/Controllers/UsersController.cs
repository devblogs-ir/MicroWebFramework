using MicroWebFramework.Models;

namespace PipelineDesignPattern.Controllers;
public class UsersController : BaseController
{
    public UsersController(HttpContext context) : base(context) { }

    IList<Users> Users = new List<Users>()
    {
        new() { Id = 1, FullName = "Farid", },
        new() { Id = 2, FullName = "Ali", },
        new() { Id = 3, FullName ="Amir", },
        new() { Id = 4, FullName = "Amin", },
    };

    public void GetAllUsers()
    {
        Ok(Users);
    }

    public void GetUserById(int id)
    {
        Ok(Users.FirstOrDefault(x => x.Id == id));
    }
}
