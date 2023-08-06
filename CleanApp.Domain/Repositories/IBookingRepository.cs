using CleanApp.Domain.Entities;

namespace CleanApp.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingEntity> Create(BookingEntity bookingEntity);

        Task<BookingEntity> Update(BookingEntity bookingEntity);

        Task<BookingEntity> Get(Guid id);

        Task Delete(Guid id);
    }
}
