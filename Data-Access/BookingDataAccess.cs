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

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(string userId)
        {
            // const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId";

            const string sql = @"SELECT b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish 
                                FROM Bookings b, UserBookings us, Users u 
                                WHERE u.UserId =@ UserId";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { UserId = userId });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisDay(string userId)
        {
            var currentDate = DateTime.Now;
            // const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @CurrentDate AND Finish >= @CurrentDate";
            const string sql = @"SELECT b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish  
                                FROM Bookings b, UserBookings ub, Users u
                                WHERE u.UserId = @UserId
                                AND Start <= @CurrentDate
                                AND Finish >= @CurrentDate
                                GROUP BY b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { UserId = userId, CurrentDate = currentDate });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisWeek(string userId)
        {
            var startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endDate = startDate.AddDays(7);
            //const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @EndDate AND Finish >= @StartDate";
            const string sql = @"SELECT b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish  
                                FROM Bookings b, UserBookings ub, Users u
                                WHERE u.UserId = @UserId
                                AND Start <= @EndDate AND Finish >= @StartDate
                                GROUP BY b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { UserId = userId, EndDate = endDate, StartDate = startDate });
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsThisMonth(string userId)
        {
            var endDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, endDay);
            var startDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //const string sql = "SELECT * FROM BOOKINGS WHERE WorkerId = @WorkerId AND Start <= @EndDate AND Finish >= @StartDate";
            const string sql = @"SELECT b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish  
                                FROM Bookings b, UserBookings ub, Users u
                                WHERE u.UserId = @UserId
                                AND Start <= @EndDate AND Finish >= @StartDate
                                GROUP BY b.BookingId, b.Customer, b.ProjektName, b.Start, b.Finish";


            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var bookings = await connection.QueryAsync<Booking>(sql, new { UserId = userId, EndDate = lastDay, StartDate = startDayOfMonth });
            return bookings;
        }

        public async Task<bool> DeleteBooking(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var isSuccess = await connection.DeleteAsync(new Booking { BookingId = id });

            return isSuccess;
        }

        public async Task<int> CreateBooking(Booking booking)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var identety = await connection.InsertAsync(booking);
            
            var userBooking = new UserBooking
            {
                BookingId = booking.BookingId,
                UserId = "test",
            };

            await connection.InsertAsync(userBooking);
            
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