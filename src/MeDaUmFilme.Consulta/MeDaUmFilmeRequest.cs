namespace MeDaUmFilme
{

    public class MeDaUmFilmeRequest
    {
        private readonly string REQUEST_API = "http://www.omdbapi.com/?";

        public string Title { get; set; }
        public string Year { get; set; }

        public string SearchUri 
        {
            get 
            {
                var random = new MeDaUmFilmeRandom();

                var uri = $"{REQUEST_API}s={Title}";

                if (string.IsNullOrWhiteSpace(Title))
                    uri = $"{REQUEST_API}s={random.GetRandomTitle}";

                if (!string.IsNullOrWhiteSpace(Year))
                    uri = $"{uri}&y={Year}";

                return uri;
            }
        }
    }
}