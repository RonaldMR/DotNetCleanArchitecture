using CleanApp.Application.Constants;
using CleanApp.Application.DTO.Booking;
using CleanApp.Application.Exceptions;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Enums;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.Booking
{
    public class CreateBookingUseCase
    {
        private readonly IBookingRepository bookingRepository;

        private readonly IUserRepository userRepository;

        public CreateBookingUseCase(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
        }

        public async Task<BookingDTO> Execute(string userEmailAddress, CreateBookingDTO request)
        {
            var existingUser = await this.userRepository.Get(userEmailAddress);

            if(existingUser == null)
            {
                throw new RelationDoesNotExists("User");
            }

            var fromDate = request.FromDate;

            var toDate = request.ToDate;

            if(fromDate == toDate)
            {
                throw new EqualToException("FromDate", "ToDate");
            }

            if (fromDate > toDate)
            {
                throw new GreaterThanException("FromDate", "ToDate");
            }

            RoomType roomType;

            var rawRoomType = request.RoomType;

            var roomTypeValid = Enum.TryParse(rawRoomType, out roomType);

            if(!roomTypeValid)
            {
                throw new NotValidException("RoomType");
            }

            short roomSize;

            var roomSizeExists = RoomTypeSizeConstants.Sizes.TryGetValue(roomType, out roomSize);

            if(!roomSizeExists)
            {
                throw new NotValidException("RoomType");
            }

            var guests = request.Guests;

            if(guests > roomSize)
            {
                throw new GreaterThanException("Guests", "RoomSize");
            }

            var status = BookingStatus.Open;

            var booking = new BookingEntity(existingUser.Id, fromDate, toDate, guests, request.BreakfastIncluded, roomType, request.Indications, status);

            booking = await this.bookingRepository.Create(booking);

            return new BookingDTO()
            {
                Id = booking.Id,
                FromDate = booking.FromDate,
                ToDate = booking.ToDate,
                Guests = booking.Guests,
                RoomType = roomType.ToString(),
                BreakfastIncluded = booking.BreakfastIncluded,
                Status = booking.Status.ToString(),
            };
        }
    }
}
