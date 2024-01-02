﻿using System.Runtime.InteropServices.ComTypes;

namespace PipelineDesignPattern;

public class ExceptionHandling : Pipe
{
    public ExceptionHandling(Action<HttpContext> next) : base(next)
    {
    }
    public ExceptionHandling() : base() { }

    public override void Handle(HttpContext context)
    {
        try
        {
            Console.WriteLine("Starting ExceptionHandling");

            if (_next is not null) _next(context);

            Console.WriteLine("Ending ExceptionHandling");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

    }
}