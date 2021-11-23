using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.IdentityServer4.IdentityConfiguration
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                Name = "RendezvousPolyCliniqueApi",
                DisplayName = "Rendez-vous PolyClinique Api",
                Description = "Permet à l'application de se connecter à l'API RendezvousPolyCliniqueApi",
                Scopes = new List<string> { "RendezVousPolyClinique.read", "RendezVousPolyClinique.write"},
                ApiSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},
                UserClaims = new List<string> {"role"}
                }
            };
        }
    }
}