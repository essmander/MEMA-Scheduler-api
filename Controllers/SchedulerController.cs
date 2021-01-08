using IdentityServer4;
using MEMA_Planning_Schedule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MEMA_Planning_Schedule.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class SchedulerController : ControllerBase
    {

        //private readonly IConfiguration _configuration;
        private readonly IBookingDataAccess _bookingDataAccess;

        public SchedulerController(IBookingDataAccess bookingDataAccess)
        {
            _bookingDataAccess = bookingDataAccess;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> Get() => Ok(await _bookingDataAccess.GetAllBookings());

        [HttpGet("{workerId}/day")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisDay(int workerId) => Ok(await _bookingDataAccess.GetBookingsThisDay(workerId));

        [HttpGet("{workerId}/week")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisWeek(int workerId) => Ok(await _bookingDataAccess.GetBookingsThisWeek(workerId));
       
        [HttpGet("{workerId}/month")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisMonth(int workerId) => Ok(await _bookingDataAccess.GetBookingsThisMonth(workerId));

        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> GetAllBookingsByUserId(int id) => Ok(await _bookingDataAccess.GetBookingsByUserId(id));

        [HttpDelete("{id}")]
        // [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingDataAccess.DeleteBooking(id);
            return deleted ? Ok() : NotFound();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            await _bookingDataAccess.CreateBooking(booking);
            return Ok();
        }

        [HttpPatch]
        // [Authorize]
        public async Task<IActionResult> Update([FromBody] Booking booking) => Ok(await _bookingDataAccess.UpdateBooking(booking));

    }
}