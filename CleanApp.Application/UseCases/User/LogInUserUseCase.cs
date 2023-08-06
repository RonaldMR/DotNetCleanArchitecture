using CleanApp.Application.DTO.User;
using CleanApp.Application.Exceptions;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.User
{
    public class LogInUserUseCase
    {
        private readonly IUserRepository repository;

        public LogInUserUseCase(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserDTO> Execute(string emailAddress, string password)
        {
            var existingUser = await this.repository.Get(emailAddress);

            if(existingUser?.Password != password)
            {
                throw new AccessDeniedException();
            }

            return new UserDTO()
            {
                Id = existingUser.Id,
                EmailAddress = existingUser.EmailAddress,
                Password = existingUser.Password,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                CreationDate = existingUser.CreationDate
            };
        }
     }
}
