using CleanApp.Domain.Entities;
using CleanApp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class PostgressUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public PostgressUserRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        private UserEntity GetBookingEntityFromReader(IDataReader reader)
        {
                return new UserEntity(new Guid(reader[0].ToString()), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5));
        }

        public async Task<UserEntity> Create(UserEntity userEntity)
        {
            await using(var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using(var cmd = new NpgsqlCommand("INSERT INTO users (id, \"firstName\", \"lastName\", \"emailAddress\", password, \"creationDate\") VALUES (@id, @firstName, @lastName, @emailAddress, @password, @creationDate) returning *", conn))
                {
                    cmd.Parameters.AddWithValue("id", userEntity.Id);
                    cmd.Parameters.AddWithValue("firstName", userEntity.FirstName);
                    cmd.Parameters.AddWithValue("lastName", userEntity.LastName);
                    cmd.Parameters.AddWithValue("emailAddress", userEntity.EmailAddress);
                    cmd.Parameters.AddWithValue("password", userEntity.Password);
                    cmd.Parameters.AddWithValue("creationDate", userEntity.CreationDate);

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            return GetBookingEntityFromReader(reader);
                        }

                        return userEntity;
                    }
                }
            }
        }

        public async Task<UserEntity?> Get(string emailAddress)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand("SELECT * FROM users WHERE \"emailAddress\" = @emailAddress", conn))
                {
                    cmd.Parameters.AddWithValue("emailAddress", emailAddress);

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            return GetBookingEntityFromReader(reader);
                        }

                        return null;
                    }
                }
            }
        }
    }
}
