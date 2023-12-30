using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.PipeLineConsoleApplication
{
    public class ProductsController(HttpContext httpContext)
    {
        public void GetAllUser()
        {
            $"Return all users".Dump();
        }
        public void GetUserById(int userId)
        {
            $"Return user by id:{userId}".Dump();
        }
    }
}
