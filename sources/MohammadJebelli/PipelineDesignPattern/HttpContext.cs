﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern;

public class HttpContext
{
    public string IP { get; set; }
    public string Url { get; set; }
    public string HttpRequest { get; set; }
    public string HttpResponse { get; set; }
}



