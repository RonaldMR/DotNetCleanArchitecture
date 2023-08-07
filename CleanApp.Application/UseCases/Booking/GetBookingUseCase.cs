using CleanApp.Application.DTO.Booking;
using CleanApp.Application.Exceptions;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.Booking
{
    public class GetBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly IUserRepository _userRepository;

        public GetBookingUseCase(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this._bookingRepository = bookingRepository;
            this._userRepository = userRepository;
        }

        public async Task<BookingDTO> Execute(string userEmailAddress, Guid id)
        {
            var existingUser = await this._userRepository.Get(userEmailAddress);

            if (existingUser == null)
            {
                throw new RelationDoesNotExists("User");
            }

            var booking = await this._bookingRepository.Get(id);

            if(booking?.UserId != existingUser.Id)
            {
                throw new NotFoundException("Booking");
            }

            return new BookingDTO()
            {
                Id = booking.Id,
                FromDate = booking.FromDate,
                ToDate = booking.ToDate,
                Guests = booking.Guests,
                RoomType = booking.RoomType.ToString(),
                BreakfastIncluded = booking.BreakfastIncluded,
                Indications = booking.Indications,
                Status = booking.Status.ToString(),
            };
        }
    }
}
