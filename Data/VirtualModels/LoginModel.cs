using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.VirtualModels
{
    public class LoginModel
    {
        [MaxLength(64), Required]
        public string UserName { get; set; }
        [MaxLength(64), Required]
        public string Password { get; set; }
        [Required]
        public bool rememberMe { get; set; }
    }
}
