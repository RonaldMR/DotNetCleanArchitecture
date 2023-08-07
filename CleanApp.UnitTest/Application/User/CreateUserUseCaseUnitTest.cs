using CleanApp.Application.DTO.User;
using CleanApp.Application.Exceptions;
using CleanApp.Application.UseCases.User;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Moq;

namespace CleanApp.UnitTest
{
    public class CreateUserUseCaseUnitTest
    {
        [Fact]
        public async void ShouldNotCreateUserWhenEmailExists()
        {
            var dtoMock = new CreateUserDTO()
            {
                FirstName = "FOO",
                LastName = "FOO",
                EmailAddress = "foo@goo.com",
                Password = "FOO"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult(userMock));

            var useCase = new CreateUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute(dtoMock);

            await Assert.ThrowsAsync<AlreadyExistsException>(act);
        }

        [Fact]
        public async void ShouldCreateUser()
        {
            var dtoMock = new CreateUserDTO()
            {
                FirstName = "FOO",
                LastName = "FOO",
                EmailAddress = "foo@goo.com",
                Password = "FOO"
            };

            var expectedUser = new UserEntity(dtoMock.FirstName, dtoMock.LastName, dtoMock.EmailAddress, dtoMock.Password);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));
            userRepositoryMock.Setup(repository => repository.Create(It.IsAny<UserEntity>())).Returns(Task.FromResult<UserEntity>(expectedUser));

            var useCase = new CreateUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute(dtoMock);

            var result = await act();

            Assert.Equal(result.FirstName, expectedUser.FirstName);
            Assert.Equal(result.LastName, expectedUser.LastName);
            Assert.Equal(result.EmailAddress, expectedUser.EmailAddress);
        }
    }
}