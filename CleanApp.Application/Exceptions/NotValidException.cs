using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class NotValidException : Exception
    {
        public NotValidException(string roomType) : base(string.Format(ExceptionConstants.NOT_VALID, roomType))
        { 
        
        }
    }
}
