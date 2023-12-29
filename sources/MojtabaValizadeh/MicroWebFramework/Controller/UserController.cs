using MicroWebFramework.Repository;

namespace MicroWebFramework;

public class UserController
{
    private readonly Data _data;

    public UserController()
    {
        _data = new Data();
    }
   
    public List<User> GetAllUser()
    {
      
        return _data.Users.ToList();
    }
    public User? GetUserById(int id)
    {
        return _data.Users.FirstOrDefault(a => a.Id == id);
    }
}