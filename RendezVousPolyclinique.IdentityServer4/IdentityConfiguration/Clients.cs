using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.IdentityServer4.IdentityConfiguration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "SimpleClient",
                    ClientName = "Client pouvant juste lire ",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("RdvClinique".Sha256())},
                    AllowedScopes = new List<string> { "RendezVousPolyClinique.read" }
                }
            };
        }

    }
}
