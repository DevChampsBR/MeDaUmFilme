using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeDaUmFilme.Language
{
    public partial class LuisResult
    {
        public LuisResult() { }

        public LuisResult(string query, IList<IntentRecommendation> intents, IList<EntityRecommendation> entities)
        {
            Query = query;
            Intents = intents;
            Entities = entities;
        }

        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "intents")]
        public IList<IntentRecommendation> Intents { get; set; }

        [JsonProperty(PropertyName = "topScoringIntent")]
        public IntentRecommendation TopScoringIntent { get; set; }

        [JsonProperty(PropertyName = "entities")]
        public IList<EntityRecommendation> Entities { get; set; }

        public virtual void Validate()
        {
            if (Query == null)
                throw new ValidationException("Query cannot be null.");
            if (Intents == null)
                throw new ValidationException("Intents cannot be null.");
            if (Entities == null)
                throw new ValidationException("Entities cannot be null.");
        }
    }
}
