using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class AccessTokenRequest
    {
        public string ApiKey { get; set; }

        public string SceretKey { get; set; }

        public ResponseBody responseBody { get; set; }
    }

    public class ResponseBody
    {
        public string transactionReference { get; set; }
        public string paymentReference { get; set; }
        public string merchantName { get; set; }
        public string apiKey { get; set; }
        public List<string> enabledPaymentMethod { get; set; }
        public string checkoutUrl { get; set; }
    }

    public class AccessTokenResponse
    {
        public class ResponseBody
        {
            public string accessToken { get; set; }
            public int expiresIn { get; set; }
        }

        public class AccessTokenRoot
        {
            public bool requestSuccessful { get; set; }
            public string responseMessage { get; set; }
            public string responseCode { get; set; }
            public ResponseBody responseBody { get; set; }
        }


        public class IncomeSplitConfig
        {
            public string subAccountCode { get; set; }
            public double feePercentage { get; set; }
            public int splitAmount { get; set; }
            public bool feeBearer { get; set; }
        }
        public class Metadata
        {
            public string name { get; set; }
            public int age { get; set; }
        }


        public class SinglePayment
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
            public List<IncomeSplitConfig> incomeSplitConfig { get; set; }
            public Metadata metadata { get; set; }
        }
        public class InitializeSplit
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
            public List<IncomeSplitConfig> incomeSplitConfig { get; set; }
            public Metadata metadata { get; set; }


            public class AccessTokenResponse
            {
                public class ResponseBody
                {
                    public string accessToken { get; set; }
                    public int expiresIn { get; set; }
                }

                public class AccessTokenRoot
                {
                    public bool requestSuccessful { get; set; }
                    public string responseMessage { get; set; }
                    public string responseCode { get; set; }
                    public ResponseBody responseBody { get; set; }
                }
            }
        }
    }
}
