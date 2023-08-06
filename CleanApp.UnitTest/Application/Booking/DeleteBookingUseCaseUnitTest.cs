using CleanApp.Application.Exceptions;
using CleanApp.Application.UseCases.Booking;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Moq;

namespace CleanApp.UnitTest
{
    public class DeleteBookingUseCaseUnitTest
    {
        [Fact]
        public async void ShouldNotDeleteBookingWhenUserDoesNotExists()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new DeleteBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<RelationDoesNotExists>(act);
        }

        [Fact]
        public async void ShouldNotDeleteBookingWhenBookingIsNotFound()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(null));

            var useCase = new DeleteBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ShouldNotDeleteBookingWhenBookingDoesNotBelongToUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var bookingMock = new BookingEntity(Guid.NewGuid(), DateTime.Now, DateTime.Now, 1, true, Domain.Enums.RoomType.Single, "FOO", BookingStatus.Open);

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(bookingMock));

            var useCase = new DeleteBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ShouldDeleteBooking()
        {
            var userIdMock = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(userIdMock, "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var bookingMock = new BookingEntity(userIdMock, DateTime.Now, DateTime.Now, 1, true, Domain.Enums.RoomType.Single, "FOO", BookingStatus.Open);

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(bookingMock));

            var useCase = new DeleteBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await act();
        }
    }
}