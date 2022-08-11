using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.FlutterWave
{
  public  class FlutterRequest
    {
        public string tx_ref { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
        public string meta { get; set; }
        public Customer customer { get; set; }
       
    }
}
