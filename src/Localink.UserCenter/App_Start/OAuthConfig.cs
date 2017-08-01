using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Localink.UserCenter.IdentityServer;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;

namespace Localink.UserCenter.App_Start
{
    /// <summary>
    /// 配置IdentityServer3
    /// </summary>
    public static class OAuthConfig
    {
        public static void Configure(IAppBuilder app)
        {
            // 配置Idsrv路由地址，以及Idsrv options
            app.Map("/identity", idsrvApp =>
            {
                //配置Client/Scope/User
                var factory = Factory.Configure();

                //配置idsrc options
                var idsrvOptions = new IdentityServerOptions
                {
                    SiteName = "Localink UserCenter",
                    SigningCertificate = Certificate.Get(),
                    Factory = factory,
                    //认证配置
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        //允许post退出后自动跳转，客户端需要配置PostLogoutRedirectUri
                        EnablePostSignOutAutoRedirect = true,
                        //第三方OP配置
                        IdentityProviders = ConfigureAdditionalIdentityProviders
                    }
                };

                idsrvApp.UseIdentityServer(idsrvOptions);
            });
        }

        private static void ConfigureAdditionalIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
                ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = "676607329068058",
                AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            };
            app.UseFacebookAuthentication(fb);

            var twitter = new TwitterAuthenticationOptions
            {
                AuthenticationType = "Twitter",
                SignInAsAuthenticationType = signInAsType,
                ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
                ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            };
            app.UseTwitterAuthentication(twitter);
        }
    }
}