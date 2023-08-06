using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class GreaterThanException: Exception
    {
        public GreaterThanException(string fromEntity, string toEntity): base(string.Format(ExceptionConstants.GREATER_THAN, fromEntity, toEntity))
        {

        }
    }
}
