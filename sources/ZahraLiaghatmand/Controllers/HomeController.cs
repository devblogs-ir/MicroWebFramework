using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern.Controllers
{
    internal class HomeController
    {
        private readonly HttpContext _context;
        public HomeController(HttpContext context)
        {
            _context = context;
        }
        public void Index()
        {
            Console.WriteLine("Welcome to Home");
            _context.Response += "Welcome to Home";
        }
    }
}
