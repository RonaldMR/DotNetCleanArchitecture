using CleanApp.Application.Exceptions;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.Booking
{
    public class DeleteBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        private readonly IUserRepository _userRepository;

        public DeleteBookingUseCase(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this._bookingRepository = bookingRepository;
            this._userRepository = userRepository;
        }

        public async Task Execute(string userEmailAddress, Guid id)
        {
            var existingUser = await this._userRepository.Get(userEmailAddress);

            if (existingUser == null)
            {
                throw new RelationDoesNotExists("User");
            }

            var booking = await this._bookingRepository.Get(id);

            if (booking?.UserId != existingUser.Id)
            {
                throw new NotFoundException("Booking");
            }

            await this._bookingRepository.Delete(booking.Id);
        }
    }
}
