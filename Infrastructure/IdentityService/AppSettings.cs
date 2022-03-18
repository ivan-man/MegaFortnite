using IdentityService.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4;

namespace IdentityService
{
    public class AppSettings
    {
        public readonly List<AllowedClientViewModel> AllowedClients = new List<AllowedClientViewModel>();

        public readonly List<IdentityResource> ApiResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            // configuration.GetSection("ApiResources").Bind(ApiResources);
        }

        public IEnumerable<Client> GetIdentityClients()
        {
            //_configuration.GetSection("AllowedClients").Bind(AllowedClients);
            // var result = AllowedClients.Select(q => new Client
            // {
            //     ClientId = q.ClientId,
            //     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            //
            //     ClientSecrets =
            //     {
            //         new Secret(q.Secret.Sha256())
            //     },
            //     AllowedScopes =
            //     {
            //         IdentityServerConstants.StandardScopes.OpenId,
            //         IdentityServerConstants.StandardScopes.Profile,
            //         IdentityServerConstants.StandardScopes.Email,
            //         IdentityServerConstants.StandardScopes.Address,
            //         "x_scope"
            //     }
            // }).ToList();

            var result = new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                    }
                },

                // interactive ASP.NET Core MVC client
                // new Client
                // {
                //     ClientId = "mvc",
                //     ClientSecrets = { new Secret("secret".Sha256()) },
                //
                //     AllowedGrantTypes = GrantTypes.Code,
                //
                //     // where to redirect to after login
                //     RedirectUris = { "https://localhost:5002/signin-oidc" },
                //
                //     // where to redirect to after logout
                //     PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                //
                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         "api1"
                //     }
                // },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "https://localhost:5003/callback.html",
                        "https://localhost:5123/callback.html",
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:5003/index.html",
                        "https://localhost:5123/lobby.html",
                        "https://localhost:5123/index.html",
                    },
                    AllowedCorsOrigins =
                    {
                        "https://localhost:5003",
                        "https://localhost:5123"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "x_scope",
                        "api1",
                    }
                }
            };

            return result;
        }

        public IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new ApiScope("api1", "My API"),
        };

        public IEnumerable<ApiResource> GetIdentityApiResources() =>
            ApiResources.Select(q =>
                new ApiResource
                {
                    Name = q.Name,
                    DisplayName = q.DisplayName,
                });

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "x_scope",
                    DisplayName = "x_scope",
                    Description = "A Custom Scope",
                    UserClaims = new[]
                    {
                        "x_scope"
                    }
                }
            };
        }
    }
}
