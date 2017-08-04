using Localink.UserCenter.AspNetIdentity.Entitys;
using Localink.UserCenter.Models.Api.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers.Api
{
    [HandleForbidden]
    [Auth(Roles = "Users")]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiBaseController
    {

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="value"></param>
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task Register(AddUserInput input)
        {
            var roleName = "Users";
            AppUser user = await UserManager.FindByNameAsync(input.UserName);

            //角色不存在创建Users
            if (!RoleManager.RoleExists(roleName))
            {
                await RoleManager.CreateAsync(new AppRole(roleName));
            }

            //用户不存在，创建用户
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    PhoneNumber = input.PhoneNumber,
                    Gender = true,
                    RegisterTime = DateTime.Now,
                    CountryCode = input.CountryCode
                };

                await UserManager.CreateAsync(user, input.Password);
                user = await UserManager.FindByNameAsync(input.UserName);
            }

            //添加用户角色
            if (!UserManager.IsInRole(user.Id, roleName))
            {
                await UserManager.AddToRoleAsync(user.Id, roleName);
            }

            //添加声明
            UserManager.AddClaim(user.Id, new Claim("first_name", user.FirstName));
            UserManager.AddClaim(user.Id, new Claim("last_name", user.LastName));
            UserManager.AddClaim(user.Id, new Claim("country_code", user.CountryCode));
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("UpdateUser")]
        [HttpPost]
        public async Task UpdateUser(UpdateUserInput input)
        {
            if (input.Id == 0)
            {
                return;
            }
            var user = UserManager.FindById(input.Id);
            if (user == null)
            {
                return;
            }
            user.Email = input.Email;
            user.PhoneNumber = input.PhoneNumber;
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            await UserManager.UpdateAsync(user);
        }
    }
}