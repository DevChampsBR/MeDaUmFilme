﻿using System;
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
            var emptyRequest = new MeDaUmFilmeRequest();

            var result = await MeDaUmFilmeSearch.GetMovie(emptyRequest);

            Assert.True(result.Contains("Batman"));
        }
    }
}