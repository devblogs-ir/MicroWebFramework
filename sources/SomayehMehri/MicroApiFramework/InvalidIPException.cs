﻿internal class InvalidIPException : Exception
{
    public InvalidIPException()
    {
    }

    public InvalidIPException(string? message) : base(message)
    {
    }
}