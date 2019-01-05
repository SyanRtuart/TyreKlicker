using TyreKlicker.Infrastructure.Identity.Models;

namespace TyreKlicker.API.Services.Token
{
    public interface IUserTokenService
    {
        Models.Token.UserToken CreateUserToken(ApplicationUser email);
    }
}