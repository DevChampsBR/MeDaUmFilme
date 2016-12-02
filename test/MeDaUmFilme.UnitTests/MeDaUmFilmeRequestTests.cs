using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeDaUmFilme.UnitTests
{

    public class MeDaUmFilmeRequestTests
    {
        [Fact]
        public async Task MeDaUmFilmeRequest_With_No_Title()
        {
            var emptyRequest = new OmbdRequest();

            Movie result = await new MeDaUmFilmeSearch().GetMovie(emptyRequest);

            Assert.True(!string.IsNullOrWhiteSpace(result.Title));
        }
    }
}
