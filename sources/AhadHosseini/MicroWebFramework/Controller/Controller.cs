using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using MicroWebFramework.Models;
using Newtonsoft.Json;

namespace MicroWebFramework.Controller
{
    public abstract class Controller
    {
        public HttpContext HttpContext { get; }
        public Controller(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
        public async void Ok(object? value)
        {
            var result = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(new OutPutModel<object>()
            {
                IsSuccess = true,
                httpStatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                _model = value
            }));
            HttpContext.Response.OutputStream.Write(result, 0, result.Length);
            HttpContext.Response.OutputStream.Close();
        }
    }
   
    public class OutPutModel<TData>
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public System.Net.HttpStatusCode httpStatusCode { get; set; }
        public TData _model { get; set; }

    }
}
