using Newtonsoft.Json;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Years
    {
        [JsonProperty("slug")] public string Slug { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}