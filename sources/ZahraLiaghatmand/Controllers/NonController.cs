using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern.Controllers;
public class NonController : BaseController
{
    public NonController(HttpContext context)
    {
        _context = context;
    }
    public void Exception()
    {
        if (this._context.Url.Split('/')[1] == "favicon.ico") {
            return;
        }
        else
        {
            _context.Response = "404: Page Not Found";
        }
    }
}
