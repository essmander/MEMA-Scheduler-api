using System.Collections.Generic;
using System.Threading.Tasks;
using MEMA_Planning_Schedule.Models;

namespace MEMA_Planning_Schedule
{
    public interface IBookingDataAccess
    {
        public Task<IEnumerable<Booking>> GetAllBookings();
        public Task<Booking> GetBooking(int id);
        public Task<IEnumerable<Booking>> GetBookingsByUserId(string userId); //NO CONTROLLER
        public Task<IEnumerable<Booking>> GetBookingsThisDay(string userId);
        public Task<IEnumerable<Booking>> GetBookingsThisWeek(string userId);
        public Task<IEnumerable<Booking>> GetBookingsThisMonth(string userId);
        public Task<bool> DeleteBooking(int id);
        public Task<int> CreateBooking(Booking booking, string userId);
        public Task<bool> UpdateBooking(Booking booking);
    }
}