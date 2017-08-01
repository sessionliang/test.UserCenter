using Localink.UserCenter.AspNetIdentity.Entitys;
using Localink.UserCenter.AspNetIdentity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.AspNetIdentity
{
    /// <summary>
    /// AppIdentity实体存储结构的数据库上下文
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim>
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public AppIdentityDbContext()
            : base("LocalinkUserCenter")
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString"></param>
        public AppIdentityDbContext(string connectString)
            : base(connectString)
        { }

        /// <summary>
        /// 静态构造器
        /// </summary>
        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        /// <summary>
        /// 静态方法，获取上下文实例
        /// </summary>
        /// <returns></returns>
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            // initial configuration will go here
            // 初始化配置将放在这儿

            #region 数据库初始化
            AppUserManager userMgr = new AppUserManager(new AppUserStore(context));
            AppRoleManager roleMgr = new AppRoleManager(new AppRoleStore(context));

            //当前用户中心管理员角色
            string roleName = "Administrators";
            //当前用户中心管理员
            string userName = "admin";
            string password = "123qwe";
            string email = "admin@example.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                user = new AppUser { UserName = userName, Email = email };
                user.Gender = true;
                user.RegisterTime = DateTime.Now;

                userMgr.Create(user,
                    password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            //普通用户角色
            string userRoleName = "User";
            if (!roleMgr.RoleExists(userRoleName))
            {
                roleMgr.Create(new AppRole(userRoleName));
            }

            #endregion

        }
    }
}