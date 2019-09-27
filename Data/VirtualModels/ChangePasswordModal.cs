using System;
using System.Collections.Generic;
using System.Text;

namespace Data.VirtualModels
{
    public class ChangePasswordModal
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string NewPassword2 { get; set; }
    }
}
