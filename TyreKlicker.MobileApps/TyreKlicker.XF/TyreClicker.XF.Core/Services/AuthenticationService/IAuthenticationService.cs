using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Authentication;

namespace TyreKlicker.XF.Core.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        string CreateLogoutRequest(string token);

        Task<UserToken> GetTokenAsync(string userName, string password);

        Task<UserToken> RefreshTokenAsync(string token);
    }
}