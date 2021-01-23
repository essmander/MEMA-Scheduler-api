using IdentityServer4;
using MEMA_Planning_Schedule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MEMA_Planning_Schedule.Controllers
{
    [Route("api/bookings")]
    public class SchedulerController : ApiController
    {

        //private readonly IConfiguration _configuration;
        private readonly IBookingDataAccess _bookingDataAccess;

        public SchedulerController(IBookingDataAccess bookingDataAccess)
        {
            _bookingDataAccess = bookingDataAccess;
        }

        [HttpGet]
        // [Authorize]
        // [Authorize(Policy =  IdentityServerConstants.LocalApi.PolicyName)]
        public async Task<IActionResult> Get() => Ok(await _bookingDataAccess.GetAllBookings());

        [HttpGet("test")]
        [Authorize(Policy =  IdentityServerConstants.LocalApi.PolicyName)]
        public string GetTest() => "TEST Worked";

        [HttpGet("testmod")]
        [Authorize(Policy =  MemaConst.Policies.Mod)]
        public string ModAuth() => "TEST MOD Worked";

        [HttpGet("day")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisDay() => Ok(await _bookingDataAccess.GetBookingsThisDay(UserId));

        [HttpGet("week")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisWeek() => Ok(await _bookingDataAccess.GetBookingsThisWeek(UserId));
       
        [HttpGet("month")]
        // [Authorize]
        public async Task<IActionResult> GetBookigsThisMonth() => Ok(await _bookingDataAccess.GetBookingsThisMonth(UserId));

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
        [Authorize(Policy =  IdentityServerConstants.LocalApi.PolicyName)]
        public async Task<IActionResult> Create([FromBody] Booking booking) => Ok(await _bookingDataAccess.CreateBooking(booking, UserEmail)); //Temp fix 

        [HttpPatch]
        [Authorize(Policy =  IdentityServerConstants.LocalApi.PolicyName)]
        // [Authorize]
        public async Task<IActionResult> Update([FromBody] Booking booking) => Ok(await _bookingDataAccess.UpdateBooking(booking));

    }
}