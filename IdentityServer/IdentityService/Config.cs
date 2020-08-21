using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityService
{
    internal class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope>
       {
            new ApiScope("MovieServiceApi", "MovieService API")
       };

        public static IEnumerable<Client> Clients =>
    new List<Client>
    {
            new Client
            {
                ClientId = "MovieServiceApi",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "MovieServiceApi" }
            }

           
    };






        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "eren",
                    Password = "123123",
                    Claims = new List<Claim>
                    {
                        new Claim("id", "1"),
                        new Claim("name", "James Bond"),
                    }
                }
            };
        }
    }
}
