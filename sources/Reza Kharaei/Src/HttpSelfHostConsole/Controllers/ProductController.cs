using System;

namespace HttpSelfHostConsole.Controllers
{
    public class ProductController
    {
        public void GetAll()
        {
            Console.WriteLine("*******************");
            Console.WriteLine("Return Products List...");
            Console.WriteLine("*******************");
        }

        public void GetById(int Id)
        {
            Console.WriteLine("*******************");
            Console.WriteLine($"Return Product ({Id.ToString()}) Details...");
            Console.WriteLine("*******************");
        }
    }
}
