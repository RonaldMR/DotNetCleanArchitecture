using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class AccessDeniedException: Exception
    {
        public AccessDeniedException() : base(ExceptionConstants.LOGIN_ACCESS_DENIED) { }
    }
}
