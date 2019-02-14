using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class WheelPair
    {
        [JsonProperty("showing_fp_only")]
        public bool ShowingFpOnly { get; set; }

        [JsonProperty("is_stock")]
        public bool IsStock { get; set; }

        public Wheel Front { get; set; }
        public Wheel Rear { get; set; }
    }
}