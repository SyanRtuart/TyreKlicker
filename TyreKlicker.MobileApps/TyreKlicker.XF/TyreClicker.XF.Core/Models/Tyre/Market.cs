using Newtonsoft.Json;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Market
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("abbr")]
        public string Abbr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_en")]
        public string NameEn { get; set; }
    }
}