using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class AlreadyExistsException: Exception
    {
        public AlreadyExistsException(string entity) : base(string.Format(ExceptionConstants.ALREADY_EXISTS, entity))
        {
            
        }
    }
}
