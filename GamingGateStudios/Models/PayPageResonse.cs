using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingGateStudios.Models
{
    public class PayPageResonse
    {
        public string result { get; set; }
        public string response_code { get; set; }
        public string payment_url { get; set; }
        public string p_id { get; set; }
        public string error_code { get; set; }
    }
}