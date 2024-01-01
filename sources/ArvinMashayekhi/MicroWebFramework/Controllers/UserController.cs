using MicroWebFramework.Common;
using MicroWebFramework.Entities;
using MicroWebFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Controllers;
public class UserController(IUserService userService)
{
    private readonly IUserService _userService = userService;
    public void GetUserById(int id)
    {
        _userService.GetById(id);
    }
}

