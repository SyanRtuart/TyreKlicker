using Newtonsoft.Json;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Wheel
    {
        [JsonProperty("tyre_pressure")] public Pressure TyrePressure { get; set; }

        public string Rim { get; set; }

        [JsonProperty("rim_diameter")] public double? RimDiameter { get; set; }

        [JsonProperty("rim_width")] public double? RimWidth { get; set; }

        [JsonProperty("rim_offset")] public double? RimOffset { get; set; }

        public string Tire { get; set; }

        [JsonProperty("tire_sizing_system")] public string TireSizingSystem { get; set; }

        [JsonProperty("tire_construction")] public string TireConstruction { get; set; }

        [JsonProperty("tire_width")] public double? TireWidth { get; set; }

        [JsonProperty("tire_aspect_ratio")] public double? TireAspectRatio { get; set; }

        [JsonProperty("tire_diameter")] public double? TireDiameter { get; set; }

        [JsonProperty("tire_section_width")] public double? TireSectionWidth { get; set; }

        [JsonProperty("tire_is_82series")] public bool TireIs82Series { get; set; }

        [JsonProperty("load_index")] public int? LoadIndex { get; set; }

        [JsonProperty("speed_index")] public string SpeedIndex { get; set; }

        //speed_index string
    }
}