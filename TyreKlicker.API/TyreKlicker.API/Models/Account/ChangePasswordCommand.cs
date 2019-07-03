using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TyreKlicker.API.Models.Account
{
    public class ChangePasswordCommand
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
