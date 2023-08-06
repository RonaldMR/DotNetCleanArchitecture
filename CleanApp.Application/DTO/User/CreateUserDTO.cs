using System.ComponentModel.DataAnnotations;

namespace CleanApp.Application.DTO.User
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(4)]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(4)]
        public required string LastName { get; set; }

        [Required]
        [MinLength(10)]
        public required string EmailAddress { get; set; }

        [Required]
        [MinLength(10)]
        public required string Password { get; set; }
    }
}
