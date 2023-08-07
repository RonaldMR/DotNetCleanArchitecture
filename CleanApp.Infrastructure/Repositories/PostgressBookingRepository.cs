using CleanApp.Domain.Constants;
using CleanApp.Domain.Entities;
using CleanApp.Domain.Enums;
using CleanApp.Domain.Repositories;
using Npgsql;
using System.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class PostgressBookingRepository : IBookingRepository
    {
        private readonly string _connectionString;

        public PostgressBookingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private BookingEntity GetBookingEntityFromReader(IDataReader reader)
        {
            return new BookingEntity(new Guid(reader.GetString(0)), new Guid(reader.GetString(1)), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt16(3), reader.GetBoolean(4), Enum.Parse<RoomType>(reader.GetString(5)), reader.GetString(6), Enum.Parse<BookingStatus>(reader.GetString(7)));
        }

        public async Task<BookingEntity> Create(BookingEntity bookingEntity)
        {
            await using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand("INSERT INTO bookings (id, userId, fromDate, toDate, breakfastIncluded, type, indications, status) VALUES (@id, @userId, @fromDate, @toDate, @breakfastIncluded, @type, @indications, @status) returning *", conn))
                {
                    cmd.Parameters.AddWithValue("id", bookingEntity.Id);
                    cmd.Parameters.AddWithValue("userId", bookingEntity.UserId);
                    cmd.Parameters.AddWithValue("fromDate", bookingEntity.FromDate);
                    cmd.Parameters.AddWithValue("toDate", bookingEntity.ToDate);
                    cmd.Parameters.AddWithValue("breakfastIncluded", bookingEntity.BreakfastIncluded);
                    cmd.Parameters.AddWithValue("type", bookingEntity.RoomType);
                    cmd.Parameters.AddWithValue("status", bookingEntity.Status);

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

                await using (var cmd = new NpgsqlCommand(" UPDATE bookings SET fromDate=@fromDate, toDate=@toDate, breakfastIncluded=@breakfastIncluded, type=@type, status=@status WHERE id = @id returning *", conn))
                {
                    cmd.Parameters.AddWithValue("id", bookingEntity.Id);
                    cmd.Parameters.AddWithValue("fromDate", bookingEntity.FromDate);
                    cmd.Parameters.AddWithValue("toDate", bookingEntity.ToDate);
                    cmd.Parameters.AddWithValue("breakfastIncluded", bookingEntity.BreakfastIncluded);
                    cmd.Parameters.AddWithValue("type", bookingEntity.RoomType);
                    cmd.Parameters.AddWithValue("status", bookingEntity.Status);

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
