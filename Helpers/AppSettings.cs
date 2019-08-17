using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular8.Helpers
{
    public class AppSettings
    {
        //Properties for JMT Token Signature
        public string Site { get; set; }
        public string Audience { get; set; }
        public string ExpireTime { get; set; }
        public string Secret { get; set; }
    }
}
