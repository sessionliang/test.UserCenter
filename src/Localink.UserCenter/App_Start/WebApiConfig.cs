using Localink.UserCenter.App_Start;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Localink.UserCenter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 设置
            var json = config.Formatters.JsonFormatter;

            json.UseDataContractJsonSerializer = false;
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" });


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new AuthorizeAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );
        }

        public static void Configure(IAppBuilder app)
        {
            //允许跨域
            app.UseCors(CorsOptions.AllowAll);


            // 配置api验证
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = IdentityConfig.IdsrvRootAddress + "identity",
                ClientId = "localink.ios.app",
                ClientSecret = "55b7bab9-c741-47f7-9d50-f5a654386029",
                RequiredScopes = new[] { "keyApi" },
                IssuerName = IdentityConfig.IdsrvRootAddress + "identity",
                SigningCertificate = IdentityServer.Certificate.Get()
            });

            // Wire Web API
            var httpConfiguration = new HttpConfiguration();
            Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);

        }
    }
}
