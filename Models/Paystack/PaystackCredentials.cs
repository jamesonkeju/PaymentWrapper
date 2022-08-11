using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Paystack
{
   public  class PaystackCredentials
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string callback_url { get; set; }
        public bool User_Production_Credentails { get; set; } = false;


    }
}
