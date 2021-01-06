using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MEMA_Planning_Schedule
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookingDataAccess, BookingDataAccess>();

            AddIdentity(services);

            services.AddControllers();

            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MEMA_Planning_Schedule", Version = "v1" });
            });

            services.AddCors(options => options.AddPolicy(MemaConst.Policies.AllCors, build => build.AllowAnyHeader()
                                                                        .AllowAnyOrigin()
                                                                        .AllowAnyMethod()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MEMA_Planning_Schedule v1"));

                

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MemaConst.Policies.AllCors);

            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(config =>
                config.UseInMemoryDatabase("DevIdentity"));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    if (_env.IsDevelopment())
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequiredLength = 4;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                    }
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
            });

            var identetyServerBuilder = services.AddIdentityServer();

            identetyServerBuilder.AddAspNetIdentity<IdentityUser>();


            if (_env.IsDevelopment())
            {
                identetyServerBuilder.AddInMemoryIdentityResources(new IdentityResource[]
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                });

                identetyServerBuilder.AddInMemoryApiResources(Config.GetApiResources());
               

                identetyServerBuilder.AddInMemoryApiScopes(new ApiScope[]
                {
                    new ApiScope(IdentityServerConstants.LocalApi.ScopeName),

                });


                identetyServerBuilder.AddInMemoryClients(new Client[]
                {
                    new Client
                    {
                        ClientId = "web-client",
                        AllowedGrantTypes = GrantTypes.Code,

                        RedirectUris = new [] { "http://localhost:3000" },
                        PostLogoutRedirectUris = new [] { "http://localhost:3000" },
                        AllowedCorsOrigins = new [] { "http://localhost:3000" },

                        AllowedScopes = new []
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.LocalApi.ScopeName,
                        },

                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        RequireClientSecret = false,


                    },
                    new Client
                    {
                        ClientId = "postman-api",
                        ClientName = "Postman Test Client",
                        AllowedGrantTypes = GrantTypes.Code,
                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                        PostLogoutRedirectUris = {"htts://www.getpostman.com"},
                        AllowedCorsOrigins = {"htts://www.getpostman.com"},
                        EnableLocalLogin = true,
                        RequirePkce = false,
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            IdentityServerConstants.LocalApi.ScopeName,
                            "postman_api"
                        },
                        ClientSecrets = new List<Secret>() { new Secret("SomeValue".Sha256()) }
                    }
                });
                identetyServerBuilder.AddInMemoryApiScopes(Config.GetApiScopes());
                identetyServerBuilder.AddDeveloperSigningCredential();
            }

            services.AddLocalApiAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(MemaConst.Policies.Mod, policy =>
                {
                    var is4Policy = options.GetPolicy(IdentityServerConstants.LocalApi.PolicyName);
                    policy.Combine(is4Policy);
                    policy.RequireClaim(ClaimTypes.Role, MemaConst.Roles.Mod);
                });
            });
        }
    }

    public struct MemaConst
    {
        public struct Policies
        {
            public const string Mod = nameof(Mod);
            public const string AllCors = nameof(AllCors);
        }
        public struct Roles
        {
            public const string Mod = nameof(Mod);
        }
    }
}
