using CleanApp.Application.DTO.Booking;
using CleanApp.Application.Exceptions;
using CleanApp.Application.UseCases.Booking;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Moq;

namespace CleanApp.UnitTest
{
    public class CreateBookingUseCaseUnitTest
    {
        [Fact]
        public async void ShouldNotCreateBookingIfRelatedUserDoesNotExist()
        {
            var dtoMock = new CreateBookingDTO()
            {
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
                Guests = 1,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "FOO"
            };

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(null));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            await Assert.ThrowsAsync<RelationDoesNotExists>(act);
        }

        [Fact]
        public async void ShouldNotCreateBookingWhenFromDateIsEqualToToDate()
        {
            var dateMock = DateTime.Now;

            var dtoMock = new CreateBookingDTO()
            {
                FromDate = dateMock,
                ToDate = dateMock,
                Guests = 1,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "FOO"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            await Assert.ThrowsAsync<EqualToException>(act);
        }

        [Fact]
        public async void ShouldNotCreateBookingWhenFromDateIsGreaterThanToDate()
        {
            var fromDateMock = new DateTime(2023, 10, 11);
            var toDateMock = new DateTime(2023, 9, 11);

            var dtoMock = new CreateBookingDTO()
            {
                FromDate = fromDateMock,
                ToDate = toDateMock,
                Guests = 1,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "FOO"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            await Assert.ThrowsAsync<GreaterThanException>(act);
        }

        [Fact]
        public async void ShouldNotCreateBookingWhenRoomTypeIsNotValid()
        {
            var fromDateMock = new DateTime(2023, 10, 11);
            var toDateMock = new DateTime(2023, 11, 11);

            var dtoMock = new CreateBookingDTO()
            {
                FromDate = fromDateMock,
                ToDate = toDateMock,
                Guests = 1,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "FOO"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            await Assert.ThrowsAsync<NotValidException>(act);
        }

        [Fact]
        public async void ShouldNotCreateBookingWhenGuestsAreGreatherThanRoomSize()
        {
            var fromDateMock = new DateTime(2023, 10, 11);
            var toDateMock = new DateTime(2023, 11, 11);

            var dtoMock = new CreateBookingDTO()
            {
                FromDate = fromDateMock,
                ToDate = toDateMock,
                Guests = 10,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "Single"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            await Assert.ThrowsAsync<GreaterThanException>(act);
        }

        [Fact]
        public async void ShouldCreateBooking()
        {
            var fromDateMock = new DateTime(2023, 10, 11);
            var toDateMock = new DateTime(2023, 11, 11);

            var dtoMock = new CreateBookingDTO()
            {
                FromDate = fromDateMock,
                ToDate = toDateMock,
                Guests = 1,
                BreakfastIncluded = true,
                Indications = "FOO",
                RoomType = "Single"
            };

            var userMock = new UserEntity(Guid.NewGuid(), "FOO", "FOO", "FOO", "FOO", DateTime.Now);

            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>())).Returns(Task.FromResult<UserEntity>(userMock));

            var bookingMock = new BookingEntity(userMock.Id, dtoMock.FromDate, dtoMock.ToDate, dtoMock.Guests, dtoMock.BreakfastIncluded, Domain.Enums.RoomType.Single, dtoMock.Indications, BookingStatus.Open);

            var bookingRepositoryMock = new Mock<IBookingRepository>();

            bookingRepositoryMock.Setup(repository => repository.Create(It.IsAny<BookingEntity>())).Returns(Task.FromResult(bookingMock));

            var useCase = new CreateBookingUseCase(bookingRepositoryMock.Object, userRepositoryMock.Object);

            var act = () => useCase.Execute("FOO", dtoMock);

            var result = await act();

            Assert.Equal(dtoMock.FromDate, result.FromDate);
            Assert.Equal(dtoMock.ToDate, result.ToDate);
            Assert.Equal(dtoMock.Guests, result.Guests);
            Assert.Equal(dtoMock.RoomType, result.RoomType);
            Assert.Equal(dtoMock.BreakfastIncluded, result.BreakfastIncluded);
            Assert.Equal(dtoMock.Indications, result.Indications);
            Assert.Equal("Open", result.Status);
        }
    }
}