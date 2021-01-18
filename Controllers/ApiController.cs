using System.Linq;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;

namespace MEMA_Planning_Schedule.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        public string UserId => GetClaim(JwtClaimTypes.Subject);
        public string UserName => GetClaim(JwtClaimTypes.PreferredUserName);
        public string UserEmail => GetClaim(JwtClaimTypes.Email);
        private string GetClaim(string claimType) => User.Claims
            .FirstOrDefault(x => x.Type.Equals(claimType))?.Value;

    }

}