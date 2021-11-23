using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.IdentityServer4.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("RendezVousPolyClinique.read", "Accès en lecture à RendezVousPolyClinique API"),
            new ApiScope("RendezVousPolyClinique.write", "Accès en écriture à RendezVousPolyClinique API"),
        };
        }
    }
}
