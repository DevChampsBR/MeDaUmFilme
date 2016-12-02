using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeDaUmFilme.Language
{
    internal partial class EntityRecommendation
    {
        public EntityRecommendation() { }

        public EntityRecommendation(string role = default(string), string entity = default(string), string type = default(string), int? startIndex = default(int?), int? endIndex = default(int?), double? score = default(double?), IDictionary<string, string> resolution = default(IDictionary<string, string>))
        {
            Role = role;
            Entity = entity;
            Type = type;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Score = score;
            Resolution = resolution;
        }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "entity")]
        public string Entity { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "startIndex")]
        public int? StartIndex { get; set; }

        [JsonProperty(PropertyName = "endIndex")]
        public int? EndIndex { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

        [JsonProperty(PropertyName = "resolution")]
        public IDictionary<string, string> Resolution { get; set; }

    }
}
