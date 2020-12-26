
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

        private readonly IConfiguration _configuration;
        private readonly IBookingDataAccess _bookingDataAccess;

        public SchedulerController(IConfiguration configuration, IBookingDataAccess bookingDataAccess)
        {
            _configuration = configuration;
            _bookingDataAccess = bookingDataAccess;
        }

       [HttpGet]
       public async Task<IActionResult> Get()
       {
            string sql = "SELECT TOP 10 * FROM Employe";

            using var connection = new SqlConnection(_configuration.GetConnectionString("MEMA"));
            var orderDetails = await connection.QueryAsync<User>(sql);
            return Ok(orderDetails);
       }

       [HttpGet]
       [Route("Bookings")]
       public async Task<IActionResult> GetAllBookings() => Ok(await _bookingDataAccess.GetAllBookings());
    }
}