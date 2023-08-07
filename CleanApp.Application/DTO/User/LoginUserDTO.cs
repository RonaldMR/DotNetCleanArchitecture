using System.ComponentModel.DataAnnotations;

namespace CleanApp.Application.DTO.User
{
    public class LoginUserDTO
    {
        [Required]
        [MinLength(10)]
        public required string EmailAddress { get; set; }

        [Required]
        [MinLength(10)]
        public required string Password { get; set; }
    }
}
