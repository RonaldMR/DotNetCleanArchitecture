using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Enums;
using CleanApp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class PostgressBookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public PostgressBookingRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            if(string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        private BookingEntity GetBookingEntityFromReader(IDataReader reader)
        {
            return new BookingEntity(new Guid(reader[0].ToString()), new Guid(reader[1].ToString()), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt16(4), reader.GetBoolean(5), Enum.Parse<RoomType>(reader.GetString(6)), reader.GetString(7), Enum.Parse<BookingStatus>(reader.GetString(8)));
        }

        public async Task<BookingEntity> Create(BookingEntity bookingEntity)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand("INSERT INTO bookings (id, \"userId\", \"fromDate\", \"toDate\", \"guests\", \"breakfastIncluded\", type, indications, status) VALUES (@id, @userId, @fromDate, @toDate, @guests, @breakfastIncluded, @type, @indications, @status) returning *", conn))
                {
                    cmd.Parameters.AddWithValue("id", bookingEntity.Id);
                    cmd.Parameters.AddWithValue("userId", bookingEntity.UserId);
                    cmd.Parameters.AddWithValue("fromDate", bookingEntity.FromDate);
                    cmd.Parameters.AddWithValue("toDate", bookingEntity.ToDate);
                    cmd.Parameters.AddWithValue("guests", bookingEntity.Guests);
                    cmd.Parameters.AddWithValue("breakfastIncluded", bookingEntity.BreakfastIncluded);
                    cmd.Parameters.AddWithValue("type", bookingEntity.RoomType.ToString());
                    cmd.Parameters.AddWithValue("indications", bookingEntity.Indications);
                    cmd.Parameters.AddWithValue("status", bookingEntity.Status.ToString());

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            return GetBookingEntityFromReader(reader);
                        }

                        return bookingEntity;
                    }
                }
            }
        }

        public async Task Delete(Guid id)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand("DELETE FROM bookings WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<BookingEntity?> Get(Guid id)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand("SELECT * FROM bookings WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

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

        public async Task<BookingEntity> Update(BookingEntity bookingEntity)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand(" UPDATE bookings SET \"fromDate\"=@fromDate, \"toDate\"=@toDate, \"guests\"=@guests, \"breakfastIncluded\"=@breakfastIncluded, type=@type, indications = @indications, status=@status WHERE id = @id returning *", conn))
                {
                    cmd.Parameters.AddWithValue("id", bookingEntity.Id);
                    cmd.Parameters.AddWithValue("fromDate", bookingEntity.FromDate);
                    cmd.Parameters.AddWithValue("toDate", bookingEntity.ToDate);
                    cmd.Parameters.AddWithValue("guests", bookingEntity.Guests);
                    cmd.Parameters.AddWithValue("breakfastIncluded", bookingEntity.BreakfastIncluded);
                    cmd.Parameters.AddWithValue("type", bookingEntity.RoomType.ToString());
                    cmd.Parameters.AddWithValue("indications", bookingEntity.Indications);
                    cmd.Parameters.AddWithValue("status", bookingEntity.Status.ToString());

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            return GetBookingEntityFromReader(reader);
                        }

                        return bookingEntity;
                    }
                }
            }
        }
    }
}
