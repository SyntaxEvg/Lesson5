using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class Root
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string Urls { get; set; }
    }
}
