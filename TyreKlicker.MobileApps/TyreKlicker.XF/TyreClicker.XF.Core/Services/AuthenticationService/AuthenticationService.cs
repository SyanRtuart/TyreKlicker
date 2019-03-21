using System;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Authentication;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestProvider _requestProvider;

        public AuthenticationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public string CreateLogoutRequest(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<UserToken> GetTokenAsync(LoginRequest loginRequest)
        {
            var token = await _requestProvider.PostAsync<UserToken, LoginRequest>(GlobalSetting.Instance.TokenEndpoint, loginRequest);

            GlobalSetting.Instance.AuthToken = token.AccessToken;

            return token;
        }

        public Task<UserToken> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<UserToken> GetTokenAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterRequest registerRequest)
        {
            await _requestProvider.PostAsync<string, RegisterRequest>(GlobalSetting.Instance.AuthenticationEndpoint + "/register", registerRequest);
        }
    }
}