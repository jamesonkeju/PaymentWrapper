using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Paystack
{
    public class InitializeResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public DataPaystack data { get; set; }
    }

    public class DataPaystack
    {
        [JsonProperty("authorization_url")]
        public string authorization_url { get; set; }

        [JsonProperty("access_code")]
        public string access_code { get; set; }

        [JsonProperty("reference")]
        public string reference { get; set; }
    }
}
