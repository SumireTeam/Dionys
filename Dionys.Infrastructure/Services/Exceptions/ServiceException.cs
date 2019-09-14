using System;

namespace Dionys.Infrastructure.Services.Exceptions
{
    public abstract class ServiceException : Exception
    {
        protected ServiceException(string message) : base(message)
        {

        }
    }
}
