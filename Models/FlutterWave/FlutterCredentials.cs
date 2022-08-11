using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.FlutterWave
{
   public  class FlutterCredentials
    {
        public string FlutterPublicKey { get;set; }
        public string FlutterWavePaymentReturnUrl { get; set; }
        public string Flutter_SercetKey { get; set; }
        public bool User_Production_Credentails { get; set; }
    }
}
