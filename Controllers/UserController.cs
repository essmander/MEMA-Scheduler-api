using System.Linq;
using IdentityModel;
using MEMA_Planning_Schedule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEMA_Planning_Schedule.Controllers
{

    [Route("api/users")]
    [Authorize(MemaConst.Policies.User)]
    public class UserController : ApiController
    {
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return BadRequest();
            }

            var user = new User
            {
                UserId = UserId,
                Username = UserName,
                Email = UserEmail
            };

            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id) => Ok();
    }

}