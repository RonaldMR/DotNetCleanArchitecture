using CleanApp.Application.DTO.User;
using CleanApp.Application.Exceptions;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.User
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _repository;

        public CreateUserUseCase(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<UserDTO> Execute(CreateUserDTO request)
        {
            var existingUser = await _repository.Get(request.EmailAddress);

            if(existingUser != null)
            {
                throw new AlreadyExistsException("User");
            }

            var user = new UserEntity(request.FirstName, request.LastName, request.EmailAddress, request.Password);

            user = await this._repository.Create(user);

            return new UserDTO()
            {
                Id = user.Id,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreationDate = user.CreationDate
            };
        }
    }
}
