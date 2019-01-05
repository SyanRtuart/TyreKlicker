using System;

namespace TyreKlicker.XF.Core
{
    public class GlobalSetting
    {
        public const string DefaultEndpoint = "https://localhost:44310";

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

        public string ClientId { get { return "xamarin"; } }

        public string ClientSecret { get { return "secret"; } }

        public string AuthToken { get; set; }

        public string AuthorizeEndpoint { get; set; }

        public string TokenEndpoint { get; set; }
        public string Callback { get; set; }

        private void UpdateEndpoint(string endpoint)
        {
            var connectBaseEndpoint = $"{endpoint}/connect";
            AuthorizeEndpoint = $"{connectBaseEndpoint}/authorize";
            TokenEndpoint = $"{connectBaseEndpoint}/token";

            var baseUri = ExtractBaseUri(endpoint);
            Callback = $"{baseUri}/xamarincallback";
        }

        private string ExtractBaseUri(string endpoint)
        {
            var uri = new Uri(endpoint);
            var baseUri = uri.GetLeftPart(System.UriPartial.Authority);

            return baseUri;
        }
    }
}