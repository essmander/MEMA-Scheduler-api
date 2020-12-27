using System.Collections.Generic;
using System.Threading.Tasks;
using MEMA_Planning_Schedule.Models;

namespace MEMA_Planning_Schedule
{
    public interface IBookingDataAccess
    {
        public Task<IEnumerable<Booking>> GetAllBookings();
        public Task<IEnumerable<Booking>> GetBookingsByUserId(int id);
        public Task<bool> DeleteBooking(int id);
        public long CreateBooking(Booking booking);
       
    }
}