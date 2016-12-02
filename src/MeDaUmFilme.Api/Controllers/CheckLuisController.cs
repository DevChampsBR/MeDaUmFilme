using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeDaUmFilme.Language;

namespace MeDaUmFilme.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckLuisController : Controller
    {
        private IAnalyzer languageAnalyzer;

        public CheckLuisController(IAnalyzer languageAnalyzer)
        {
            this.languageAnalyzer = languageAnalyzer;
        }

        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            var intent = await languageAnalyzer.AnalyzeAsync(id);
            return intent.Entities["Titulo"];
        }
    }
}
