using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.IdentityServer4.IdentityConfiguration
{
    public class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "56892347",
                Username = "Admin",
                Password = "Admin",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Email, "admin@Rendezvouspolyclinique.com"),
                    new Claim(JwtClaimTypes.Role, "admin"),
                    new Claim(JwtClaimTypes.WebSite, "https://www.Rendezvouspolyclinique.com")
                }
            }
        };
        }
    }
}
