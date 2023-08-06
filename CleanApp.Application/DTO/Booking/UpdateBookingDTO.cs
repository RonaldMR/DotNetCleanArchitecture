using System.ComponentModel.DataAnnotations;

namespace CleanApp.Application.DTO.Booking
{
    public class UpdateBookingDTO
    {
        [Required]
        public required string Status { get; set; }
    }
}
