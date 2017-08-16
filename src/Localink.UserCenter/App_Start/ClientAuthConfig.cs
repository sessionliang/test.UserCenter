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
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;
using IdentityServer3.Core;
using IdentityModel.Client;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Localink.UserCenter.AspNetIdentity.Managers;
using IdentityServer3.AccessTokenValidation;

namespace Localink.UserCenter.App_Start
{
    /// <summary>
    /// 配置MVC Client
    /// </summary>
    public static class ClientAuthConfig
    {
        public static void Configure(IAppBuilder app)
        {


            //1. 配置CookieAuthentication
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                CookieName = "Localink.UserCenter.Cookies",
                AuthenticationType = "Cookies"
            });

            //2. 配置OIDC
            app.UseOpenIdConnectAuthentication(new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions
            {
                //认证地址
                Authority = IdentityConfig.IdsrvRootAddress + "identity",
                //客户端标识
                ClientId = "mvc",
                //客户端秘钥，这里因为是自己调用自己，所以没有设置秘钥
                //ClientSecret = "",
                //认证回调
                RedirectUri = IdentityConfig.IdsrvRootAddress,
                //客户端需要的授权范围scope，提供给用户确认时，选择的(注意：openid必选。openid是区分这个请求是认证请求，而不是授权请求的。)
                Scope = "openid profile roles keyApi",
                //返回类型：id_token(OIDC的认证的用户身份信息)   token(access_token, 授权的访问令牌)
                ResponseType = "id_token token",
                //登录后，以Cookie方式认证
                SignInAsAuthenticationType = "Cookies",
                //设置令牌到期时间
                UseTokenLifetime = false,

                //配置修改claims
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    #region 配置其他适用的claim
                    SecurityTokenValidated = async n =>
                             {

                                 var nid = new ClaimsIdentity(
                                     n.AuthenticationTicket.Identity.AuthenticationType,
                                     Constants.ClaimTypes.GivenName,
                                     Constants.ClaimTypes.Role);

                                 // get userinfo data
                                 var userInfoClient = new UserInfoClient(
                                          new Uri(n.Options.Authority + "/connect/userinfo"), n.ProtocolMessage.AccessToken);

                                 var userInfo = await userInfoClient.GetAsync();
                                 userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                                 // keep the id_token for logout
                                 nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                                 // add access token for sample API
                                 nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                                 // keep track of access token expiration
                                 nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));



                                 n.AuthenticationTicket = new AuthenticationTicket(
                                     nid,
                                     n.AuthenticationTicket.Properties);
                             },

                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.FromResult(0);
                    }

                    #endregion
                }
            });

            //3. 设置claim的名称为短名称
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            //4. 配置ResourceAuthorization
            app.UseResourceAuthorization(new AuthorizationManager());

        }

    }
}