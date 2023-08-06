namespace CleanApp.Application.DTO.Booking
{
    public class BookingDTO
    {
        public Guid Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public short Guests { get; set; }

        public bool BreakfastIncluded { get; set; }

        public required string RoomType { get; set; }

        public string? Indications { get; set; }

        public required string Status { get; set; }
    }
}
