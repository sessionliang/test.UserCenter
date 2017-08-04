using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.Models.Api.Users
{
    public class UpdateUserInput
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}