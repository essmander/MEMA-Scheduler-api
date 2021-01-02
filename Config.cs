using System.Collections.Generic;
using IdentityServer4.Models;

namespace MEMA_Planning_Schedule
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                new ApiResource("postman_api", "Postman Test Resource")
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(name: "postman_api",   displayName: "Read your data."),
            };
        }
    }
}