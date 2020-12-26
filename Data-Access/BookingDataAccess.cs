using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MEMA_Planning_Schedule.Models;
using Microsoft.Extensions.Configuration;

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
           const string sql = "SELECT * FROM BOOKINGS";

           using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
           var bookings = await connection.QueryAsync<Booking>(sql);

           return bookings;
        }
    }
}