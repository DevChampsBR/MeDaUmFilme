using System.Collections.Generic;

namespace MeDaUmFilme.Language
{
    public class Intent
    {
        public string Name { get; set; }
        public IDictionary<string, string> Entities { get; set; }
    }
}
