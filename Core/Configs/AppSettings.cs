using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Configs
{
    public class AppSettings
    {
        public string IdentityUrl { get; set; }
        public string PaystackUrl { get; set; }
        public string PaystackKey { get; set; }
        public string ImageServiceUrl { get; set; }
        public bool IsSmsLive { get; set; }
        public string OtpServiceUrl { get; set; }       
        public string[] CorsUrls { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public int ExpiredMinute { get; set; }
        public string NubanUrl { get; set; }
    }
}
