using Localink.UserCenter.AspNetIdentity.Entitys;
using Localink.UserCenter.Common;
using Localink.UserCenter.Models.Api;
using Localink.UserCenter.Models.Api.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers.Api
{
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
        public async Task<dynamic> Register(AddUserInput input)
        {
            try
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
                return Json(ApiResult.CreateSuccessResult(new
                {
                    UserName = user.UserName,
                    Phone = user.PhoneNumber,
                    EmailAddress = user.Email,
                    CountryCode = user.CountryCode
                }, User.Identity.IsAuthenticated));
            }
            catch (Exception ex)
            {
                return Json(ApiResult.CreateErrorResult(ex, User.Identity.IsAuthenticated));
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("UpdateUser")]
        [HttpPost]
        public async Task<dynamic> UpdateUser(UpdateUserInput input)
        {
            try
            {
                var user = UserManager.FindById(UserId);

                user.Email = input.Email;
                user.PhoneNumber = input.PhoneNumber;
                user.ForeName = input.Name;
                user.SurName = input.FamilyName;
                user.Address = input.Address;
                await UserManager.UpdateAsync(user);

                return Json(ApiResult.CreateSuccessResult(new
                {
                    UserName = user.UserName,
                    Phone = user.PhoneNumber,
                    EmailAddress = user.Email,
                    CountryCode = user.CountryCode
                }, User.Identity.IsAuthenticated));
            }
            catch (Exception ex)
            {
                return Json(ApiResult.CreateErrorResult(ex, User.Identity.IsAuthenticated));
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePwd")]
        public async Task<dynamic> UpdatePwd(UpdatePwdInput input)
        {
            try
            {
                if (input.NewPassword != input.ConfirmPassword)
                {
                    throw new Exception("Two input password is not consistent");
                }

                var result = await UserManager.ChangePasswordAsync(UserId, input.Password, input.NewPassword);

                if (result.Succeeded)
                    return Json(ApiResult.CreateSuccessResult(null, User.Identity.IsAuthenticated));
                else
                    return Json(ApiResult.CreateErrorResult(String.Join(".", result.Errors), User.Identity.IsAuthenticated));

            }
            catch (Exception ex)
            {
                return Json(ApiResult.CreateErrorResult(ex, User.Identity.IsAuthenticated));
            }
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadPicture")]
        public async Task<dynamic> UploadPicture()
        {
            try
            {
                var path = "upload/picture/";
                var fileName = UserId + ".png";
                //头像路径:  ./upload/picture/userId.png
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    PathUtils.CreateWhenPathIsNotExists(path);
                    FileUtils.DeleteIfFileIsExists(path + fileName);
                    //文件
                    var file = HttpContext.Current.Request.Files[0];
                    file.SaveAs(PathUtils.EnsureAbsolutePath(path + fileName));
                }

                //用户信息
                var user = await UserManager.FindByIdAsync(UserId);
                user.Picture = path + fileName;
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                    return Json(ApiResult.CreateSuccessResult(null, User.Identity.IsAuthenticated));
                else
                    return Json(ApiResult.CreateErrorResult(String.Join(".", result.Errors), User.Identity.IsAuthenticated));
            }
            catch (Exception ex)
            {
                return Json(ApiResult.CreateErrorResult(ex, User.Identity.IsAuthenticated));
            }

        }
    }
}