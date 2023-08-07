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

        public async Task<UserDTO> Execute(LoginUserDTO request)
        {
            var existingUser = await this.repository.Get(request.EmailAddress);

            if(existingUser?.Password != request.Password)
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
