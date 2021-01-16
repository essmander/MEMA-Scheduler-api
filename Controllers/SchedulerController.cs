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

        [HttpGet("test")]
        [Authorize(Policy =  IdentityServerConstants.LocalApi.PolicyName)]
        public string GetTest() => "TEST Worked";

        [HttpGet("testmod")]
        [Authorize(Policy =  MemaConst.Policies.Mod)]
        public string ModAuth() => "TEST MOD Worked";

        [HttpGet("{userId}/day")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisDay(string userId) => Ok(await _bookingDataAccess.GetBookingsThisDay(userId));

        [HttpGet("{userId}/week")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisWeek(string userId) => Ok(await _bookingDataAccess.GetBookingsThisWeek(userId));
       
        [HttpGet("{userId}/month")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisMonth(string userId) => Ok(await _bookingDataAccess.GetBookingsThisMonth(userId));

        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> GetAllBookingsByUserId(string id) => Ok(await _bookingDataAccess.GetBookingsByUserId(id));

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