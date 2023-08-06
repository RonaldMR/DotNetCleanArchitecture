using CleanApp.Application.Exceptions;
using CleanApp.Application.UseCases.Booking;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Moq;

namespace CleanApp.UnitTest
{
    public class GetBookingUseCaseUnitTest
    {
        [Fact]
        public async void ShouldNotGetBookingWhenUserDoesNotExists()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new GetBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<RelationDoesNotExists>(act);
        }

        [Fact]
        public async void ShouldNotGetBookingWhenBookingIsNotFound()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(null));

            var useCase = new GetBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ShouldNotGetBookingWhenBookingDoesNotBelongToUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var bookingMock = new BookingEntity(Guid.NewGuid(), DateTime.Now, DateTime.Now, 1, true, Domain.Enums.RoomType.Single, "FOO", BookingStatus.Open);

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(bookingMock));

            var useCase = new GetBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void ShouldGetBooking()
        {
            var userIdMock = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();

            var userMock = new UserEntity(userIdMock, "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var bookingMock = new BookingEntity(userIdMock, DateTime.Now, DateTime.Now, 1, true, Domain.Enums.RoomType.Single, "FOO", BookingStatus.Open);

            bookingRepositoryMock.Setup(repository => repository.Get(It.IsAny<Guid>())).Returns(Task.FromResult<BookingEntity>(bookingMock));

            var useCase = new GetBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", Guid.NewGuid());

            var result = await act();

            Assert.Equal(bookingMock.FromDate, result.FromDate);
            Assert.Equal(bookingMock.ToDate, result.ToDate);
            Assert.Equal(bookingMock.Guests, result.Guests);
            Assert.Equal("Single", result.RoomType);
            Assert.Equal(bookingMock.BreakfastIncluded, result.BreakfastIncluded);
            Assert.Equal(bookingMock.Indications, result.Indications);
            Assert.Equal("Open", result.Status);
        }
    }
}