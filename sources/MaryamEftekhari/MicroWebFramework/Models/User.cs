using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class UserService
    {
        public IList<User> GetUsers()
        {
            List<User> users = new List<User>();
            var user1 = new User
            {
                Id = 1,
                Name = "Maryam Eftekhari",
                Age = 14,
                BirthDay = DateTime.Now
            };
            users.Add(user1);

            var user2 = new User
            {
                Id = 2,
                Name = "Marzieh Eftekhari",
                Age = 5,
                BirthDay = Convert.ToDateTime("1996-03-20")
            };
            users.Add(user2);

            var user3 = new User
            {
                Id = 3,
                Name = "Masoume Eftekhari",
                Age = 5,
                BirthDay = Convert.ToDateTime("2018-05-20")
            };
            users.Add(user3);


            return users;
        }
    }
    
}
