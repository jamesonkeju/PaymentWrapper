using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Remita
{

    public class InitializeRemitaRequest
    {
        public string amount { get; set; }
            public string ReferenceNo { get; set; }

    }

    
    public class RequeryRemitaModel
    {
        public string OrderId { get; set; }
        public string RRR { get; set; }
        public string amount { get; set; }
    }
   

    
    
    public class RemitaPaymentResponseModel
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string Hash { get; set; }
        public string MerchantId { get; set; }
    }
 

    public class RemitaResponse
    {
        public class Datum
        {
            public string rrr { get; set; }
            public string responseMessage { get; set; }
        }

        public class Root
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<Datum> data { get; set; }
        }
    }

    public class Generated_RRR_Response
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
    }

}
