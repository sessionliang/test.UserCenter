using Localink.UserCenter.App_Start;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Localink.UserCenter.Startup))]
namespace Localink.UserCenter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //配置IdentityServer3.0
            OAuthConfig.Configure(app);

            //配置客户端MVC Client
            ClientAuthConfig.Configure(app);
        }
    }
}