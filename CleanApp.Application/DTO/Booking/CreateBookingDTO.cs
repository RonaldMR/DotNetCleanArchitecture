using System.ComponentModel.DataAnnotations;

namespace CleanApp.Application.DTO.Booking
{
    public class CreateBookingDTO
    {
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [Range(1, short.MaxValue)]
        public short Guests { get; set; }

        public bool BreakfastIncluded { get; set; }

        public string? Indications { get; set; }

        [Required]
        public required string RoomType {  get; set; }
    }
}
