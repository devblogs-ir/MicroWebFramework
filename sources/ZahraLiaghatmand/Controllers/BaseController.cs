using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern.Controllers;

public abstract  class BaseController
{
    protected HttpContext _context = null!;
}
