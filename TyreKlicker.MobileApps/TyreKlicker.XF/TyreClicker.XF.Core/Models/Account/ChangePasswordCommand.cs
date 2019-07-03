using System;
using System.Collections.Generic;
using System.Text;

namespace TyreKlicker.XF.Core.Models.Account
{
    public class ChangePasswordCommand
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
