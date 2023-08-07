using CleanApp.Application.DTO.User;
using CleanApp.Application.Exceptions;
using CleanApp.Application.UseCases.User;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Moq;

namespace CleanApp.UnitTest
{
    public class UserUnitTest
    {
        [Fact]
        public async void ShouldNotLoginWhenUserIsNotValid()
        {
            var loginUserDTOMock = new LoginUserDTO()
            {
                EmailAddress = "FOO",
                Password = "FOO"
            };

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute(loginUserDTOMock);

            await Assert.ThrowsAsync<AccessDeniedException>(act);
        }

        [Fact]
        public async void ShouldNotLoginWhenUserIsValidAndPasswordIsNotValid()
        {
            var loginUserDTOMock = new LoginUserDTO()
            {
                EmailAddress = "FOO",
                Password = "ANOTHERFOOPASSWORD"
            };

            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOOPASSWORD", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute(loginUserDTOMock);

            await Assert.ThrowsAsync<AccessDeniedException>(act);
        }

        [Fact]
        public async void ShouldLoginWhenUserAndPasswordAreValid()
        {
            var loginUserDTOMock = new LoginUserDTO()
            {
                EmailAddress = "FOO",
                Password = "FOOPASSWORD"
            };

            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOOPASSWORD", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute(loginUserDTOMock);

            var result = await act();

            Assert.Equal(result.FirstName, userMock.FirstName);
            Assert.Equal(result.LastName, userMock.LastName);
            Assert.Equal(result.EmailAddress, userMock.EmailAddress);
        }
    }
}