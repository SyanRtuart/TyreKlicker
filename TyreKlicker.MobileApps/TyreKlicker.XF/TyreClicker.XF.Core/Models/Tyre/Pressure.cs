using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Pressure
    {
        [JsonProperty("bar")]
        public double Bar { get; set; }

        [JsonProperty("pso")]
        public double Psi { get; set; }

        [JsonProperty("kPa")]
        public double Kpa { get; set; }
    }
}