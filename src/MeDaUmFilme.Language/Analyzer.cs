using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeDaUmFilme.Language
{
    public interface IAnalyzer: IDisposable
    {
        Task<Intent> AnalyzeAsync(string text);
    }

    public class Analyzer : IAnalyzer
    {
        private HttpClient client = new HttpClient();
        private const string baseUriTemplate = "https://api.projectoxford.ai/luis/v2.0/apps/{0}?subscription-key={1}&verbose=true&q=";
        private string baseUri;
        public Analyzer(Guid appId, string subscriptionKey)
        {
            Console.WriteLine($"AppId: {appId.ToString()}, sub key: {subscriptionKey}");
            baseUri = string.Format(baseUriTemplate, appId.ToString(), subscriptionKey);
        }

        public async Task<Intent> AnalyzeAsync(string text)
        {
            var encodedText = System.Text.Encodings.Web.UrlEncoder.Default.Encode(text);
            using (var result = await client.GetAsync(baseUri + encodedText))
            {
                if (!result.IsSuccessStatusCode)
                    throw new Exception($"Error analyzing language. Status: {result.StatusCode}.");
                var content = await result.Content.ReadAsStringAsync();
                var luisResult = await Task.Factory.StartNew(() => Newtonsoft.Json.JsonConvert.DeserializeObject<LuisResult>(content));
                return new Intent
                {
                    Name = luisResult.TopScoringIntent.Intent,
                    Entities = luisResult.Entities.ToDictionary(e => e.Type, e => e.Entity)
                };
            }
        }

        public void Dispose() => client.Dispose();
    }
}
