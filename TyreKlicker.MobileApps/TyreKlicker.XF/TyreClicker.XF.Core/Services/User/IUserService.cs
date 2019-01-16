using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.User;

namespace TyreKlicker.XF.Core.Services.User
{
    public interface IUserService
    {
        Task<UserModel> GetUser(string email);
    }
}