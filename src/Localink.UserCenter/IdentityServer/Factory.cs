using IdentityServer3.Core.Configuration;
using Localink.UserCenter.AspNetIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.IdentityServer
{
    /// <summary>
    /// 提供IdentityServerServiceFactory实例
    /// 这里配置Client/Scope/User Service
    /// </summary>
    public class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory();

            /*****************
             * 测试环境时，配置为内存模式。
             * 正式环境时，配置为数据库模式。
             * *****************/

            #region 测试环境
            //factory.UseInMemoryUsers(Users.Get())
            //            .UseInMemoryClients(Clients.Get())
            //            .UseInMemoryScopes(Scopes.Get());
            #endregion

            #region 正式环境
            //目前Client, Scope存储在内存中，后面会改为数据库
            factory.UseInMemoryClients(Clients.Get())
                        .UseInMemoryScopes(Scopes.Get());

            //配置UserStore
            factory.ConfigureCustomUserService();
            #endregion

            return factory;
        }
    }
}