using Newtonsoft.Json;

namespace TyreKlicker.XF.Core.Exceptions
{
    public class ErrorDetails
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("scenario")]
        public string Scenario { get; set; }
    }
}