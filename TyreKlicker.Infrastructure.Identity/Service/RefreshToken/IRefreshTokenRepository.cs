namespace TyreKlicker.Infrastructure.Identity.Service.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        Models.RefreshToken GetRefreshToken(string token);

        void SaveRefreshToken(Models.RefreshToken refreshToken);

        void RevokeRefreshToken(Models.RefreshToken refreshToken);
    }
}