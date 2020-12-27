using System.Data.SqlClient;
using MEMA_Planning_Schedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;

namespace MEMA_Planning_Schedule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulerController : ControllerBase
    {

        //private readonly IConfiguration _configuration;
        private readonly IBookingDataAccess _bookingDataAccess;

        public SchedulerController(IBookingDataAccess bookingDataAccess)
        {
            _bookingDataAccess = bookingDataAccess;
        }

        // [HttpGet]
        // public async Task<IActionResult> Get()
        // {
        //     string sql = "SELECT TOP 10 * FROM Employe";

        //     using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
        //     var orderDetails = await connection.QueryAsync<User>(sql);
        //     return Ok(orderDetails);
        // }

        [HttpGet]
        [Route("Bookings")]
        public async Task<IActionResult> Get() => Ok(await _bookingDataAccess.GetAllBookings());

        [HttpGet]
        [Route("Bookings/{id}")]
        public async Task<IActionResult> GetAllBookingsByUserId(int id) => Ok(await _bookingDataAccess.GetBookingsByUserId(id));

        [HttpDelete]
        [Route("Bookings/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookingDataAccess.DeleteBooking(id);
            return deleted ? Ok() : NotFound();
        }

        [HttpPost]
        [Route("Booking")]
        public IActionResult Post([FromBody]Booking booking) 
        {
            var created = _bookingDataAccess.CreateBooking(booking);
            return created == 1 ? Ok() : NotFound();
        }
    }
}