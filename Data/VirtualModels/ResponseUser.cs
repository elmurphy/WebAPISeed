using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.VirtualModels
{
    public class ResponseUser
    {
        public string Id;
        public string UserName;
        public string Email;
        public string PhoneNumber;
        public bool Admin;
        public static ResponseUser FromUser(ApplicationUser user)
        {
            var result = new ResponseUser();

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.Email = user.Email;
            result.PhoneNumber = user.PhoneNumber;

            return result;
        }
    }
}
