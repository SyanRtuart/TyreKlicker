using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Generation
    {
        public string Name { get; set; }

        public IEnumerable<Body> Bodies { get; set; }

        [JsonProperty("start_year")]
        public int StartYear { get; set; }

        [JsonProperty("end_year")]
        public int EndYear { get; set; }

        public IEnumerable<int> Years { get; set; }
    }
}