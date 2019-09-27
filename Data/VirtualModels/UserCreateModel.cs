using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.VirtualModels
{
    public class UserCreateModel : ResponseUser
    {
        public string Password { get; set; }
        public static ApplicationUser ToUser(UserCreateModel user)
        {
            var result = new ApplicationUser();

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.Email = user.Email;
            result.PhoneNumber = user.PhoneNumber;
            result.SecurityStamp = Guid.NewGuid().ToString();

            return result;
        }
    }
}
