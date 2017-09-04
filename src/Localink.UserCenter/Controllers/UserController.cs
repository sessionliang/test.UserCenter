using Localink.UserCenter.AspNetIdentity.Entitys;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers
{
    [HandleForbidden]
    [Auth(Roles = "Administrators")]
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            var user = new AppUser();
            return View(user);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(user);
        }

        public async Task<ActionResult> Detail(long id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(user);
        }

        public async Task<ActionResult> UpdatePwd(long id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new AppUser();
                    TryUpdateModel<AppUser>(user, form);
                    user.RegisterTime = DateTime.Now;
                    var result = await UserManager.CreateAsync(user, "123123");
                    if (result.Succeeded)
                    {
                        user = await UserManager.FindByNameAsync(user.UserName);
                        if (!await UserManager.IsInRoleAsync(user.Id, "Users"))
                        {
                            await UserManager.AddToRoleAsync(user.Id, "Users");
                        }
                        return Json(new { Success = true });
                    }
                    else
                        return Json(new { Success = false, Message = result.Errors });
                }
                return Json(new { Success = false, Message = "表单校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(long id, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);
                    TryUpdateModel<AppUser>(user, form);
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return Json(new { Success = true });
                    else
                        return Json(new { Success = false, Message = result.Errors });
                }
                return Json(new { Success = false, Message = "表单校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        var result = await UserManager.DeleteAsync(user);
                        if (result.Succeeded)
                            return Json(new { Success = true });
                        else
                            return Json(new { Success = false, Message = result.Errors });
                    }

                }
                return Json(new { Success = false, Message = "表单校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePwd(long id, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);


                    var provider = new DpapiDataProtectionProvider("mvc");
                    UserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, long>(provider.Create("UserToken"));

                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var result = await UserManager.ResetPasswordAsync(user.Id, token, form["newpassword"]);
                    if (result.Succeeded)
                        return Json(new { Success = true });
                    else
                        return Json(new { Success = false, Message = result.Errors });
                }
                return Json(new { Success = false, Message = "表单校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }

        }

        [AllowAnonymous]
        public async Task<ActionResult> ChangePwd(string email, string token)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var result = await UserManager.VerifyUserTokenAsync(user.Id, "change password", token);
            if (result)
            {
                return View(user);
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePwd(long id, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(id);


                    var provider = new DpapiDataProtectionProvider("mvc");
                    UserManager.UserTokenProvider = new DataProtectorTokenProvider<AppUser, long>(provider.Create("UserToken"));

                    var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var result = await UserManager.ResetPasswordAsync(user.Id, token, form["newpassword"]);
                    if (result.Succeeded)
                        return Json(new { Success = true });
                    else
                        return Json(new { Success = false, Message = result.Errors });
                }
                return Json(new { Success = false, Message = "表单校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }

        }
    }
}