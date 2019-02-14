using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace TyreKlicker.XF.Core.Models.Tyre
{
    public class Vehicle
    {
        public Market Market { get; set; }

        public string Body { get; set; }

        public string Trim { get; set; }

        public string Slug { get; set; }

        public Generation Generation { get; set; }

        [JsonProperty("stud_holes")]
        public int? StudHoles { get; set; }

        public double? Pcd { get; set; }

        [JsonProperty("centre_bore")]
        public double? CentreBore { get; set; }

        [JsonProperty("lock_type")]
        public string LockType { get; set; }

        [JsonProperty("lock_text")]
        public string LockText { get; set; }

        [JsonProperty("bolt_pattern")]
        public string BoltPattern { get; set; }

        public Power Power { get; set; }

        [JsonProperty("engine_type")]
        public string EngineType { get; set; }

        [JsonProperty("fuel")]
        public string Fuel { get; set; }

        public IEnumerable<WheelPair> Wheels { get; set; }
    }
}

//stud_holes integernull
//Number of stud holes(e.g. 5, can be null)

//pcd numbernull
//Pitch circle diameter, mm(e.g. 105, can be null)

//centre_bore numbernull
//Centre bore diameter, mm(e.g. 48.1, can be null)

//lock_type string
//Enum:Array[2]
//lock_text   stringnull
//Lock thread size(e.g.M12 x 1.25, can be null)

//bolt_pattern string
//Bolt pattern(e.g. 5x105, can be N/A)