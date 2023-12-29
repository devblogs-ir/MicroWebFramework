
using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern
{
    public class OrderController
    {
        private readonly HttpContext _context;

        public OrderController(HttpContext context)
        {
            _context = context;            
        }

        public string GetAll()
        {
            return $"Return all orders for IP: {_context.IP}";
        }
        public string GetById(string id)
        {
            return $"Return order by id {id}";
        }
    }
}
