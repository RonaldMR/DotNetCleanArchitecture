using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class NewBookingStatusException: Exception
    {
        public NewBookingStatusException(string from, string to): base(string.Format(ExceptionConstants.NEW_BOOKING_STATUS, from, to)) 
        { 
        
        }
    }
}
