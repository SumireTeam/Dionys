namespace Dionys.Infrastructure.Services.Exceptions
{
    public class NotFoundEntityServiceException : ServiceException
    {
        public NotFoundEntityServiceException(string message) : base(message)
        {
        }

        public NotFoundEntityServiceException()
        {
        }

        public NotFoundEntityServiceException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
