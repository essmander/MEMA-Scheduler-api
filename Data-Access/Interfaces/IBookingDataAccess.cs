using System.Collections.Generic;
using System.Threading.Tasks;
using MEMA_Planning_Schedule.Models;

namespace MEMA_Planning_Schedule
{
    public interface IBookingDataAccess
    {
        public Task<IEnumerable<Booking>> GetAllBookings();
    }
}