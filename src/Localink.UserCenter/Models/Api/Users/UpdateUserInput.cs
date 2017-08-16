using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.Models.Api.Users
{
    public class UpdateUserInput
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
    }
}