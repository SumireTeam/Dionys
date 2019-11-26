using System;

namespace Dionys.Infrastructure.Services.Exceptions
{
    public abstract class ServiceException : Exception
    {
        protected ServiceException(string message) : base(message)
        {

        }

        protected ServiceException()
        {
        }

        protected ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
