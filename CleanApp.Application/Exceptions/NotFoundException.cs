using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string entity): base(string.Format(ExceptionConstants.NOT_FOUND, entity)) 
        { 

        }
    }
}
