using Newtonsoft.Json;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Power
    {
        [JsonProperty("PS")] public double Ps { get; set; }

        [JsonProperty("hp")] public double Hp { get; set; }

        [JsonProperty("kW")] public double Kw { get; set; }
    }
}