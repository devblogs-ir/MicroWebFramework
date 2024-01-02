using MicroWebFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Services
{
    class UserServices
    {
        public List<User> GetAll() 
        { 
            var users = new List<User>();
            users.Add(new User()
            {
                firstName = "Mohamad",
                lastName = "Karimi"
            });

            users.Add(new User()
            {
                firstName = "Nabi",
                lastName = "Karampoor"
            });

            users.Add(new User()
            {
                firstName = "Fatemeh",
                lastName = "Vahdati"
            });


            return users; 
        }

        public User GetUserById(int id)
        {
            var useres = GetAll();
            return useres[id];
        }
    }
}
