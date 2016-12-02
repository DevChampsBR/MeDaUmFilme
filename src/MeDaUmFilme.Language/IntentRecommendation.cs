using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeDaUmFilme.Language
{
    public partial class IntentRecommendation
    {
        public IntentRecommendation() { }

        public IntentRecommendation(string intent = default(string), double? score = default(double?), IList<Action> actions = default(IList<Action>))
        {
            Intent = intent;
            Score = score;
            Actions = actions;
        }

        [JsonProperty(PropertyName = "intent")]
        public string Intent { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

        [JsonProperty(PropertyName = "actions")]
        public IList<Action> Actions { get; set; }
    }
}