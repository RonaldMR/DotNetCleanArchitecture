namespace CleanApp.Application.DTO.User
{
    public class UserDTO
    {
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string EmailAddress { get; set; }

        public required string Password { get; set; }

        public required DateTime CreationDate { get; set; }
    }
}
