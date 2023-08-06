using CleanApp.Application.Constants;
using CleanApp.Application.DTO.Booking;
using CleanApp.Application.Exceptions;
using CleanApp.Domain.Constants;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.Booking
{
    public class UpdateBookingUseCase
    {
        private readonly IBookingRepository bookingRepository;

        private readonly IUserRepository userRepository;

        public UpdateBookingUseCase(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
        }

        public async Task<BookingDTO> Execute(string userEmailAddress, Guid id, UpdateBookingDTO request)
        {
            var rawStatus = request.Status;

            BookingStatus status;

            var statusIsValid = Enum.TryParse(rawStatus, out status);

            if(!statusIsValid)
            {
                throw new NotValidException("BookingStatus");
            }

            var existingUser = await this.userRepository.Get(userEmailAddress);

            if (existingUser == null)
            {
                throw new RelationDoesNotExists("User");
            }

            var booking = await this.bookingRepository.Get(id);

            if (booking?.UserId != existingUser.Id)
            {
                throw new NotFoundException("Booking");
            }

            BookingStatus[] newPossibleStatus;

            var flowExists = BookingStatusFlowConstant.BookingStatusFlow.TryGetValue(booking.Status, out newPossibleStatus);

            if(!flowExists)
            {
                throw new NewBookingStatusException(booking.Status.ToString(), status.ToString());
            }

            if(!newPossibleStatus.Contains(status))
            {
                throw new NewBookingStatusException(booking.Status.ToString(), status.ToString());
            }

            booking.Status = status;

            booking = await this.bookingRepository.Update(booking);

            return new BookingDTO()
            {
                Id = booking.Id,
                FromDate = booking.FromDate,
                ToDate = booking.ToDate,
                Guests = booking.Guests,
                RoomType = booking.Type.ToString(),
                BreakfastIncluded = booking.BreakfastIncluded,
                Status = booking.Status.ToString(),
            };
        }
    }
}
