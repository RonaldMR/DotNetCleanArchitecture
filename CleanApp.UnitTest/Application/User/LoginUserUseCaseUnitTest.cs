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
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", "FOO");

            await Assert.ThrowsAsync<AccessDeniedException>(act);
        }

        [Fact]
        public async void ShouldNotLoginWhenUserIsValidAndPasswordIsNotValid()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOOPASSWORD", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", "ANOTHERFOOPASSWORD");

            await Assert.ThrowsAsync<AccessDeniedException>(act);
        }

        [Fact]
        public async void ShouldLoginWhenUserAndPasswordAreValid()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOOPASSWORD", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var useCase = new LogInUserUseCase(userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", "FOOPASSWORD");

            var result = await act();

            Assert.Equal(result.FirstName, userMock.FirstName);
            Assert.Equal(result.LastName, userMock.LastName);
            Assert.Equal(result.EmailAddress, userMock.EmailAddress);
            Assert.Equal(result.Password, userMock.Password);
        }
    }
}