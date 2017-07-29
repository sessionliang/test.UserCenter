using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Localink.UserCenter.IdentityServer
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>() {
                new InMemoryUser() {
                    Username = "test",
                    Password = "123123",
                    Subject = "1",

                    Claims = new [] {
                        new Claim(Constants.ClaimTypes.GivenName, "gname"),
                        new Claim(Constants.ClaimTypes.FamilyName, "fname"),
                        new Claim(Constants.ClaimTypes.Role, "Geek"),
                        new Claim(Constants.ClaimTypes.Role, "Foo")
                    }
                }
            };
        }
    }
}