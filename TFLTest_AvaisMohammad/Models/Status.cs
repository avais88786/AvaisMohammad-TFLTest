using Newtonsoft.Json;

namespace TFLTest_AvaisMohammad.Models
{
    public abstract class Status
    {
        [JsonIgnore]
        public bool Found { get; set; }

        [JsonIgnore]
        public string FriendlyMessage { get; set; }

        [JsonProperty("$type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Get readable status of the concrete object
        /// </summary>
        /// <returns></returns>
        public abstract void GetStatusInfo();
    }
}
