using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransityEasy.Api.Core.Options
{
    public class TranslinkOptions
    {
        public string ApiKey { get; set; }
        public string BaseApiUrl { get; set; }
        public int TimeoutInSec { get; set; }
    }
}
