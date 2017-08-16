using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.IdentityServer
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope> {
                new Scope
                {
                    Enabled = true,
                    Name="roles",
                    Type=ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role", alwaysInclude : true)
                    },
                    IncludeAllClaimsForUser = true
                },
                new Scope
                {
                    Enabled=true,
                    Name= "keyApi",
                    DisplayName = "关键性API",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };

            scopes.AddRange(StandardScopes.All);

            return scopes;
        }
    }
}