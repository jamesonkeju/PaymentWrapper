using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Remita
{
   public  class RemitaCredentials
    {

        public string merchantId { get; set; }
        public string apiKey { get; set; }
        public string serviceTypeId { get; set; }
        public bool User_Production_Credentails { get; set; } = false;
    }
}
