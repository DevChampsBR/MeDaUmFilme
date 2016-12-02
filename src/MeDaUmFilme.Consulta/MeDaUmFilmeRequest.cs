using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeDaUmFilme {

    public static class Ramdons
    {
        public static IEnumerable<string> Get()
        {
            return new List<string>()
            {
                "Batman",
                "Frozen",


            };
        }

    }

    public class MeDaUmFilmeRequest
    {
        private readonly string REQUEST_API = "http://www.omdbapi.com/?";

        public string Title { get; set; }
        public string Year { get; set; }

        public string SearchUri 
        {
            get 
            {
                var uri = $"{REQUEST_API}s={Title}";

                if (string.IsNullOrWhiteSpace(Title))
                    uri = $"{REQUEST_API}s={Ramdons.Get().FirstOrDefault()}";

                if (!string.IsNullOrWhiteSpace(Year))
                    uri = $"{uri}&y={Year}";

                return uri;
            }
        }




    }

    public class MeDaUmFilmeSearch
    {
        
        public async Task<string> GetMovie(MeDaUmFilmeRequest request)
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