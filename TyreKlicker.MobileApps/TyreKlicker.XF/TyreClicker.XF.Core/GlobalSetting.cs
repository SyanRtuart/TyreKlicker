using System;
using System.IdentityModel.Tokens.Jwt;
using TyreKlicker.XF.Core.Helpers;

namespace TyreKlicker.XF.Core
{
    public class GlobalSetting
    {
        public const string DefaultEndpoint = "http://192.168.0.6:45455";
        //public const string DefaultEndpoint = "http://tyreklickerapi-dev.eu-west-2.elasticbeanstalk.com";

        private string _baseIdentityEndpoint;

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";

            BaseIdentityEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public string BaseIdentityEndpoint
        {
            get => _baseIdentityEndpoint;
            set
            {
                _baseIdentityEndpoint = value;
                UpdateEndpoint(_baseIdentityEndpoint);
            }
        }

        public Guid CurrentLoggedInUserId
        {
            get
            {
                var jwtToken = new JwtSecurityToken(Settings.AccessToken);

                return Guid.Parse(jwtToken.Subject);
            }
        }

        public string AuthToken { get; set; }

        public string AuthenticationEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string OrderEndpoint { get; set; }
        public string OrdersEndpoint { get; set; }

        public string UserEndPoint { get; set; }
        public string UsersEndPoint { get; set; }

        public string AddressEndPoint { get; set; }
        public string AddressesEndPoint { get; set; }

        public string EncryptionPassword => "ThisWillBeChanged";

        private void UpdateEndpoint(string endpoint)
        {
            var connectBaseEndpoint = $"{endpoint}/connect";
            AuthenticationEndpoint = $"{endpoint}/api/account";
            TokenEndpoint = $"{endpoint}/api/account/tokenlogin";
            OrderEndpoint = $"{endpoint}/api/order";
            OrdersEndpoint = $"{endpoint}/api/order";
            UserEndPoint = $"{endpoint}/api/user";
            UsersEndPoint = $"{endpoint}/api/users";
            AddressEndPoint = $"{endpoint}/api/address";
            AddressesEndPoint = $"{endpoint}/api/addresses";
        }
    }
}