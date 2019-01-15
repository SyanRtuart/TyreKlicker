namespace TyreKlicker.XF.Core
{
    public class GlobalSetting
    {
        public const string DefaultEndpoint = "http://192.168.0.6:45455";

        private string _baseIdentityEndpoint;

        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";

            BaseIdentityEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public string BaseIdentityEndpoint
        {
            get { return _baseIdentityEndpoint; }
            set
            {
                _baseIdentityEndpoint = value;
                UpdateEndpoint(_baseIdentityEndpoint);
            }
        }

        public string AuthToken { get; set; }

        public string AuthorizeEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string OrdersEndpoint { get; set; }

        public string EncryptionPassword => "ThisWillBeChanged";

        private void UpdateEndpoint(string endpoint)
        {
            var connectBaseEndpoint = $"{endpoint}/connect";
            AuthorizeEndpoint = $"{endpoint}/authorize";
            TokenEndpoint = $"{endpoint}/api/account/tokenlogin";
            OrdersEndpoint = $"{endpoint}/api/orders";
        }
    }
}