// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes()
        {
            var Scope = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["Scope"];
            return new ApiScope[]
            {
                new ApiScope(Scope, "My API"),
                new ApiScope("scope"),
            };
        }

        public static IEnumerable<Client> Clients()
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var ClientId = settings.GetSection("AppSettings")["ClientId"];
            var ClientSecret = settings.GetSection("AppSettings")["ClientSecret"];
            var Scope = settings.GetSection("AppSettings")["Scope"];

            return new Client[]
            {
                new Client
                {
                    ClientId = ClientId,

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(ClientSecret.Sha256()) },

                    AllowedScopes = { Scope }
                }

            };
        }
    }
}

