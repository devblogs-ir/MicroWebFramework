using MicroWebFramework.Context;
using MicroWebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Controllers
{
    public class UsersController
    {
        public IList<User> GetAll()
        {
           UserService userService = new UserService();
           return userService.GetUsers();
        }

        public User GetById(int userId)
        {
            UserService userService = new UserService();
            return userService
                .GetUsers()
                .SingleOrDefault(u => u.Id == userId);
        }
    }
}
