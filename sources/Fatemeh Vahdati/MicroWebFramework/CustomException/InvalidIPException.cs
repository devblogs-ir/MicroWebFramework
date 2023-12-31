// See https://aka.ms/new-console-template for more information

using System.Runtime.Serialization;


namespace MicroWebFramework.CustomException
{
    public class InvalidIPException : Exception
    {
        public InvalidIPException()
        {
        }

        public InvalidIPException(string? message) : base(message)
        {
        }

        public InvalidIPException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidIPException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}