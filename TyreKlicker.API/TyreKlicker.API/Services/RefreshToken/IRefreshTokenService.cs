using TyreKlicker.Infrastructure.Identity.Models;
using RefreshToken = TyreKlicker.API.Models.Token.RefreshToken;

namespace TyreKlicker.API.Services.Token
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken(ApplicationUser user);

        void RevokeRefreshToken(string token);

        RefreshToken GetRefreshToken(string token);
    }
}