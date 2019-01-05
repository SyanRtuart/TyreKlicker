using System.Linq;
using TyreKlicker.Infrastructure.Identity.Data;

namespace TyreKlicker.Infrastructure.Identity.Service.RefreshToken
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppIdentityDbContext _context;

        public RefreshTokenRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public Models.RefreshToken GetRefreshToken(string token)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.Token == token);
        }

        public void SaveRefreshToken(Models.RefreshToken refreshToken)
        {
            var existingToken = _context.RefreshTokens.SingleOrDefault(i => i.UserId == refreshToken.UserId);

            if (existingToken != null)
            {
                _context.RefreshTokens.Remove(existingToken);
                _context.SaveChanges();
            }
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public void RevokeRefreshToken(Models.RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
        }
    }
}