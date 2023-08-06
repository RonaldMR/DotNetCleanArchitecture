using CleanApp.Domain.Entities;

namespace CleanApp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> Create(UserEntity userEntity);

        Task<UserEntity> Get(string emailAddress);
    }
}
