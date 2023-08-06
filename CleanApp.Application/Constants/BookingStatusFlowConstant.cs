using CleanApp.Domain.Constants;

namespace CleanApp.Application.Constants
{
    public static class BookingStatusFlowConstant
    {
        public static Dictionary<BookingStatus, BookingStatus[]> BookingStatusFlow = new()
        {
            { BookingStatus.Open, new BookingStatus[] { BookingStatus.Confirmed, BookingStatus.Cancelled } },
            { BookingStatus.Confirmed, new BookingStatus[] { BookingStatus.Closed } },
            { BookingStatus.Cancelled, new BookingStatus[] {  } },
            { BookingStatus.Closed, new BookingStatus[] { } }
        };
    }
}
