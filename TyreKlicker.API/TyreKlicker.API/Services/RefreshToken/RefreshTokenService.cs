using Microsoft.AspNetCore.Identity;
using System;
using TyreKlicker.Infrastructure.Identity.Models;
using RefreshToken = TyreKlicker.API.Models.Token.RefreshToken;

namespace TyreKlicker.API.Services.Token
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly Infrastructure.Identity.Service.RefreshToken.IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IPasswordHasher<ApplicationUser> passwordHasher,
            Infrastructure.Identity.Service.RefreshToken.IRefreshTokenRepository refreshTokenService)
        {
            _passwordHasher = passwordHasher;
            _refreshTokenRepository = refreshTokenService;
        }

        public string GenerateRefreshToken(ApplicationUser user)
        {
            var refreshToken = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            var myRefreshToken = new RefreshToken { Token = refreshToken };
            _refreshTokenRepository.SaveRefreshToken(new Infrastructure.Identity.Models.RefreshToken
            {
                User = user,
                Token = myRefreshToken.Token,
                IssuedUtc = DateTime.UtcNow,
                UserId = user.Id,
                Email = user.Email,
                ExpiresUtc = DateTime.UtcNow.Add(TimeSpan.FromDays(30))
            });

            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            var refreshToken = _refreshTokenRepository.GetRefreshToken(token);
            _refreshTokenRepository.RevokeRefreshToken(refreshToken);
        }

        public RefreshToken GetRefreshToken(string token)
        {
            var refreshToken = _refreshTokenRepository.GetRefreshToken(token);
            var tokenToReturn = new RefreshToken();

            if (refreshToken != null)
            {
                tokenToReturn.Email = refreshToken.Email;
                tokenToReturn.Token = refreshToken.Token;
            }

            return tokenToReturn;
        }
    }
}