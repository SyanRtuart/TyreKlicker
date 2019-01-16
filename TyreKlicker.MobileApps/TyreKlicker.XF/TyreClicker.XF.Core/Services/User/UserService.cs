using System.Threading.Tasks;
using System.Web;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.User;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestProvider _requestProvider;

        private const string ApiUrlBase = "api/users";

        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UserModel> GetUser(string email)
        {
            var encoded = HttpUtility.UrlEncode(email);

            var uri = UriHelper.CombineUri(GlobalSetting.Instance.BaseIdentityEndpoint, $"{ApiUrlBase}/Email?email=" + HttpUtility.UrlEncode(email));

            var result = await _requestProvider.GetAsync<UserModel>(uri, Settings.AccessToken);

            return result;
        }
    }
}