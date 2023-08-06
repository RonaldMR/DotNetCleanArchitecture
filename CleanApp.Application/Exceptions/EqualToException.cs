using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class EqualToException: Exception
    {
        public EqualToException(string fromEntity, string toEntity) : base(string.Format(ExceptionConstants.EQUAL_TO, fromEntity, toEntity))
        {

        }
    }
}
