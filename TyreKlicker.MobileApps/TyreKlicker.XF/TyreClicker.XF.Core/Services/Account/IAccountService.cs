using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Account;

namespace TyreKlicker.XF.Core.Services.Account
{
    public interface IAccountService
    {
        Task<ChangePasswordCommand> ChangePassword(string token, ChangePasswordCommand command);
    }
}
