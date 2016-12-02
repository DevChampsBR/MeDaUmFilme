using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeDaUmFilme.Language;

namespace MeDaUmFilme.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckOmdbController : Controller
    {
        private IAnalyzer languageAnalyzer;
        private IMeDaUmFilmeSearch meDaUmFilmeSearch;

        public CheckOmdbController(IAnalyzer languageAnalyzer, IMeDaUmFilmeSearch meDaUmFilmeSearch)
        {
            this.languageAnalyzer = languageAnalyzer;
            this.meDaUmFilmeSearch = meDaUmFilmeSearch;
        }

        [HttpGet("{id}")]
        public async Task<Movie> Get(string id)
        {
            var intent = await languageAnalyzer.AnalyzeAsync(id);

            var omdbRequest = new OmbdRequest()
            {
                Title = intent.Entities["Titulo"]
            };

            var movie = await meDaUmFilmeSearch.GetMovie(omdbRequest);

            return movie;
        }
    }
}
