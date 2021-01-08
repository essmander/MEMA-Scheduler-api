using System.Collections.Generic;
using System.Threading.Tasks;
using MEMA_Planning_Schedule.Models;

namespace MEMA_Planning_Schedule
{
    public interface IBookingDataAccess
    {
        public Task<IEnumerable<Booking>> GetAllBookings();
        public Task<Booking> GetBooking(int id);
        public Task<IEnumerable<Booking>> GetBookingsByUserId(int id); //NO CONTROLLER
        public Task<IEnumerable<Booking>> GetBookingsThisDay(int workerId);
        public Task<IEnumerable<Booking>> GetBookingsThisWeek(int workerId);
        public Task<IEnumerable<Booking>> GetBookingsThisMonth(int workerId);
        public Task<bool> DeleteBooking(int id);
        public Task<int> CreateBooking(Booking booking);
        public Task<bool> UpdateBooking(Booking booking);
    }
}