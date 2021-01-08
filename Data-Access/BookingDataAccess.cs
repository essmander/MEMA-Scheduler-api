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

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int workerId)
        {
            const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { WorkerId = workerId });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisDay(int workerId)
        {
            var currentDate = DateTime.Now;
            const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @CurrentDate AND Finish >= @CurrentDate";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { WorkerId = workerId, CurrentDate = currentDate });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisWeek(int workerId)
        {
            var startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);
            const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @EndDate AND Finish >= @StartDate";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { WorkerId = workerId, EndDate = endDate, StartDate = startDate });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisMonth(int workerId)
        {
            var endDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, endDay);
            var startDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @EndDate AND Finish >= @StartDate";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { WorkerId = workerId, EndDate = lastDay, StartDate = startDayOfMonth });
            return bookings;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var isSuccess = await connection.DeleteAsync(new Booking { Id = id });

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