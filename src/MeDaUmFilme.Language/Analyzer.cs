using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeDaUmFilme.Language
{
    public class Analyzer : IDisposable
    {
        private HttpClient client = new HttpClient();
        private const string baseUriTemplate = "https://api.projectoxford.ai/luis/v2.0/apps/{0}?subscription-key={1}&verbose=true&q=";
        private string baseUri;
        public Analyzer(Guid appId, string subscriptionKey)
        {
            baseUri = string.Format(baseUri, appId.ToString(), subscriptionKey);
        }

        public async Task<Intent> AnalyzeAsync(string text)
        {
            var encodedText = System.Text.Encodings.Web.UrlEncoder.Default.Encode(text);
            var result = await client.GetAsync(baseUri + encodedText);
            if (!result.IsSuccessStatusCode)
                throw new Exception($"Error analyzing language. Status: {result.StatusCode}.");
            var content = await result.RequestMessage.Content.ReadAsStringAsync();
            var luisResult = await Task.Factory.StartNew(() => Newtonsoft.Json.JsonConvert.DeserializeObject<LuisResult>(content));
            return new Intent
            {
                Name = luisResult.TopScoringIntent.Intent,
                Entities = luisResult.Entities.ToDictionary(e => e.Type, e => e.Entity)
            };
        }

        public void Dispose() => client.Dispose();
    }
}
