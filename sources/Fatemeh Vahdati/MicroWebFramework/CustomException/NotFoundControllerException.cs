using System.Runtime.Serialization;

namespace MicroWebFramework.CustomException
{
    [Serializable]
    public class NotFoundControllerException : Exception
    {
        public NotFoundControllerException()
        {
        }

        public NotFoundControllerException(string? message) : base(message)
        {
        }

        public NotFoundControllerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundControllerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}