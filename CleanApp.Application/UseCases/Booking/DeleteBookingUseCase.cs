using CleanApp.Application.Exceptions;
using CleanApp.Domain.Repositories;

namespace CleanApp.Application.UseCases.Booking
{
    public class DeleteBookingUseCase
    {
        private readonly IBookingRepository bookingRepository;

        private readonly IUserRepository userRepository;

        public DeleteBookingUseCase(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            this.bookingRepository = bookingRepository;
            this.userRepository = userRepository;
        }

        public async Task Execute(string userEmailAddress, Guid id)
        {
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

            await this.bookingRepository.Delete(booking.Id);
        }
    }
}
