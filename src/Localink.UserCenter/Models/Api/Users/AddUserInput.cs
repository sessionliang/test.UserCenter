using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.Models.Api.Users
{
    public class AddUserInput
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string CountryCode { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
    }
}