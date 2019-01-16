using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TyreKlicker.API.Services.Token;
using TyreKlicker.Infrastructure.Identity.Models;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace TyreKlicker.API.Services.UserToken
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public UserTokenService(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public Models.Token.UserToken CreateUserToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

            var token = new JwtSecurityToken
            (
                issuer: "TyreKlicker.API",
                audience: "TyreKlicker.XF",
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)

            );
            return new Models.Token.UserToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo,
                RefreshToken = _refreshTokenService.GenerateRefreshToken(user)
            };
        }
    }
}