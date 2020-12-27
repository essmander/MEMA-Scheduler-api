using System;
using Dapper;
using Dapper.Contrib;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MEMA_Planning_Schedule.Models;
using Microsoft.Extensions.Configuration;
using Dapper.Contrib.Extensions;

namespace MEMA_Planning_Schedule
{
    public class BookingDataAccess : IBookingDataAccess
    {
        private readonly IConfiguration _configuration;
        public BookingDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
           using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
           var bookings = await connection.GetAllAsync<Booking>();

           return bookings;
        }

       public async Task<Booking> GetBooking(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var booking = await connection.GetAsync<Booking>(id);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int id)
        {
            const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @Id";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new{Id = id});
            return bookings;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var isSuccess = await connection.DeleteAsync(new Booking {Id = id});
            
            return isSuccess;
        }

        public async Task<int> CreateBooking(Booking booking)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var identety = await connection.InsertAsync(booking);
            return identety;
        }

        public async Task<bool> UpdateBooking(Booking booking)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var isSuccess = await connection.UpdateAsync(booking);
            return isSuccess;
        }      
    }
}