using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class InitializeTransaction
    {
        public class LoginRequest
        {
            public string ApiKey { get; set; }

            public string SceretKey { get; set; }

            public ResponseBody responseBody { get; set; }

        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Initialize
        {
            public double amount { get; set; }
            public string customerName { get; set; }
            public string customerEmail { get; set; }
            public string paymentReference { get; set; }
            public string paymentDescription { get; set; }
            public string currencyCode { get; set; }
            public string contractCode { get; set; }
            public string redirectUrl { get; set; }
            public List<string> paymentMethods { get; set; }
        }



        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ResponseBody
        {
            public string transactionReference { get; set; }
            public string paymentReference { get; set; }
            public string merchantName { get; set; }
            public string apiKey { get; set; }
            public List<string> enabledPaymentMethod { get; set; }
            public string checkoutUrl { get; set; }
        }

        public class InitializeResponse
        {
            public bool requestSuccessful { get; set; }
            public string responseMessage { get; set; }
            public string responseCode { get; set; }
            public ResponseBody responseBody { get; set; }
        }
    }
}
