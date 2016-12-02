using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeDaUmFilme.Language
{
    public partial class EntityRecommendation
    {
        /// <summary>
        /// Initializes a new instance of the EntityRecommendation class.
        /// </summary>
        public EntityRecommendation() { }

        /// <summary>
        /// Initializes a new instance of the EntityRecommendation class.
        /// </summary>
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

        /// <summary>
        /// Role of the entity.
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Entity extracted by LUIS.
        /// </summary>
        [JsonProperty(PropertyName = "entity")]
        public string Entity { get; set; }

        /// <summary>
        /// Type of the entity.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Start index of the entity in the LUIS query string.
        /// </summary>
        [JsonProperty(PropertyName = "startIndex")]
        public int? StartIndex { get; set; }

        /// <summary>
        /// End index of the entity in the LUIS query string.
        /// </summary>
        [JsonProperty(PropertyName = "endIndex")]
        public int? EndIndex { get; set; }

        /// <summary>
        /// Score assigned by LUIS to detected entity.
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

        /// <summary>
        /// A machine readable dictionary with more information about the
        /// entity Look at builtin.datetime for an example in the
        /// https://www.luis.ai/Help#PreBuiltEntities.
        /// </summary>
        [JsonProperty(PropertyName = "resolution")]
        public IDictionary<string, string> Resolution { get; set; }

    }
}
