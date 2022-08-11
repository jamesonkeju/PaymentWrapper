using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Paystack  
{
    public class InitializeRequest
    {

        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }


    }


    public class InitializeRequestPayload
    {
        [JsonProperty("callback_url")]
        public string callback_url { get; set; }

        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("reference")]
        public string reference { get; set; }

        //[JsonProperty("bearer")]
        //public string bearer { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }



        //[JsonProperty("subaccount")]
        //public string subaccount { get; set; }

        [JsonProperty("transaction_charge")]
        public int transaction_charge { get; set; }

        public string Key { get; set; }
    }

    public class CreateTransferRequest
    {
        public string type { get; set; }
        public string name { get; set; }
        public string account_number { get; set; }
        public string bank_code { get; set; }
        public string currency { get; set; }
    }
    public class TransferRequest
    {
        public string source { get; set; }
        public string reason { get; set; }
        public string amount { get; set; }
        public string recipient { get; set; }
    }
    public class MobilePaymentPayload
    {

        public string TransactionReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public int[] FeeIds { get; set; }
        public string Source { get; set; }
        public int PaymentSourceApplicationId { get; set; }
        public bool Status { get; set; }
    }
    public class PaystackResponse
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
