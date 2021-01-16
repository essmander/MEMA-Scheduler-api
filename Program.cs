using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MEMA_Planning_Schedule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
               //  var identityContext = scope.ServiceProvider.GetRequiredService<ApiIdentityDbContext>();
                 var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                 var user = new IdentityUser("test"){Email = "test@test.com"};
                 userMgr.CreateAsync(user, "password").GetAwaiter().GetResult();

                 var mod = new IdentityUser("mod"){Email="mod@test.com"};
                 //mod.Email = "fredriklg3@gmail.com";
                 userMgr.CreateAsync(mod, "password").GetAwaiter().GetResult();
                 userMgr.AddClaimAsync(mod, 
                    new Claim(MemaConst.Claims.Role,
                        MemaConst.Roles.Mod)
                    ).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
