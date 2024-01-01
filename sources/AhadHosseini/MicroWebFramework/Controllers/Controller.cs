using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using MicroWebFramework.Models;
using Newtonsoft.Json;

namespace MicroWebFramework.Controllers
{
    public abstract class Controller
    {
        public HttpContext Context { get; }
        public Controller(HttpContext httpContext)
        {
            Context = httpContext;
        }
        public async void Ok(object? value)
        {
            var result = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(new Result<object>()
            {
                IsSuccess = true,
                HttpStatus = HttpStatusCode.OK,
                ErrorMessage = "",
                Model = value
            }));
            Context.Response.OutputStream.Write(result, 0, result.Length);
            Context.Response.OutputStream.Close();
        }
    }
   
    public class Result<TData>
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public TData Model { get; set; }

    }
}
