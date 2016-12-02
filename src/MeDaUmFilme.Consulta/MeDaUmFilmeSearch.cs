using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeDaUmFilme
{
    public interface IMeDaUmFilmeSearch : IDisposable
    {
        Task<Movie> GetMovie(OmbdRequest request);
    }

    public class MeDaUmFilmeSearch : IMeDaUmFilmeSearch
    {
        HttpClient client = new HttpClient();

        public async Task<Movie> GetMovie(OmbdRequest request)
        {
            var movie = new Movie();

            var response = await client.GetAsync(request.SearchUri);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var omdbResult = Newtonsoft.Json.JsonConvert.DeserializeObject<OmdbResult>(result);

                return omdbResult.Search.FirstOrDefault();
            }

            return movie;
        }

        public void Dispose() => client.Dispose();
    }
}
