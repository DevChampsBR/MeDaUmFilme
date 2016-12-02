using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDaUmFilme
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
    }

    public class OmdbResult
    {
        public List<Movie> Search { get; set; }
    }
}
