using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TFLTest_AvaisMohammad.Models
{
    public class RoadStatus : Status
    {
        [JsonProperty("statusSeverity")]
        public string StatusSeverity { get; set; }

        [JsonProperty("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonProperty("bounds")]
        public string Bounds { get; set; }

        [JsonProperty("envelope")]
        public string Envelope { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public static List<RoadStatus> FromJson(string json) => JsonConvert.DeserializeObject<List<RoadStatus>>(json);

        /// <summary>
        /// Get readable info for the roadstatus
        /// </summary>
        /// <returns></returns>
        public override void GetStatusInfo()
        {
            Console.WriteLine($"The status of Road {this.DisplayName} is as follows");
            Console.WriteLine($"\tRoad Status is {this.StatusSeverity}");
            Console.WriteLine($"\tRoad Status Description is {this.StatusSeverityDescription}");
        }
    }
}
