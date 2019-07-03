using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Account;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IRequestProvider _requestProvider;

        public AccountService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ChangePasswordCommand> ChangePassword(string token, ChangePasswordCommand command)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.AccountEndPoint, "/ChangePassword");

            return await _requestProvider.PostAsync(uri, command, token);
        }
    }
}
