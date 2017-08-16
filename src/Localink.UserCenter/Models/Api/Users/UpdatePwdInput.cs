using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.Models.Api.Users
{
    public class UpdatePwdInput
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}