using System.Net.Http;
using System.Threading.Tasks;

namespace MeDaUmFilme.Consulta
{
    public static class MeDaUmFilmeSearch
    {
        public static async Task<string> GetMovie(MeDaUmFilmeRequest request)
        {
            string json = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(request.SearchUri);

                if (response.IsSuccessStatusCode)
                    json = await response.Content.ReadAsStringAsync();

                return json;
            }

        }
    }
}
