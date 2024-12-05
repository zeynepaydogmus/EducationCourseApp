// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        //aud için apiResources
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog") { Scopes = { "catalog_fullpermission" } },
            new ApiResource("resource_photo_stock") { Scopes = { "photo_stock_fullpermission" } },
            new ApiResource("resource_basket") { Scopes = { "basket_fullpermission" } },
            new ApiResource("resource_discount") { Scopes = { "discount_fullpermission" } },
            new ApiResource("resource_order") { Scopes = { "order_fullpermission" } },
            new ApiResource("resource_fakepayment") { Scopes = { "fakepayment_fullpermission" } },
            new ApiResource("resource_gateway") { Scopes = { "gateway_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
            
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource()
                       {
                           Name = "roles" ,
                           DisplayName = "Roles",
                           Description = "User Permission",
                           UserClaims = new []{"role"}
                       }
                   };

        //permission
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API full erişim"),
                new ApiScope("photo_stock_fullpermission","Photostock API full erişim"),
                new ApiScope("basket_fullpermission","Basket API full erişim"),
                new ApiScope("discount_fullpermission","Discount API full erişim"),
                new ApiScope("order_fullpermission","Order API full erişim"),
                new ApiScope("fakepayment_fullpermission","FakePayment API full erişim"),
                new ApiScope("gateway_fullpermission","Gateway API full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        //asp.net core mvc token dagitan
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "catalog_fullpermission","photo_stock_fullpermission","gateway_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                    
                },
                new Client()
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {
                        "basket_fullpermission",
                        "discount_fullpermission",
                        "order_fullpermission",
                        "fakepayment_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.StandardScopes.Email, 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile, 
                        IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles"
                    },
                    AccessTokenLifetime = 1*60*60, //1 hour
                    RefreshTokenExpiration = TokenExpiration.Absolute, AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}