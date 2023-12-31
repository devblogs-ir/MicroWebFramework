
using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern;

public class UserController
{
    private readonly HttpContext _context;

    public UserController(HttpContext context)
    {
        _context = context;
    }

    public string GetAll() => $"Return all users for IP: {_context.IP}";
    public string GetById(string id) => $"Return user by id {id}";

}

