using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class ReserveAccountResponse
    {

        public class Account
        {
            public string bankCode { get; set; }
            public string bankName { get; set; }
            public string accountNumber { get; set; }
            public string accountName { get; set; }
        }

        public class ResponseBody
        {
            public string contractCode { get; set; }
            public string accountReference { get; set; }
            public string accountName { get; set; }
            public string currencyCode { get; set; }
            public string customerEmail { get; set; }
            public string customerName { get; set; }
            public List<Account> accounts { get; set; }
            public string collectionChannel { get; set; }
            public string reservationReference { get; set; }
            public string reservedAccountType { get; set; }
            public string status { get; set; }
            public string createdOn { get; set; }
            public List<object> incomeSplitConfig { get; set; }
            public string bvn { get; set; }
            public bool restrictPaymentSource { get; set; }
        }

        public class Root
        {
            public bool requestSuccessful { get; set; }
            public string responseMessage { get; set; }
            public string responseCode { get; set; }
            public ResponseBody responseBody { get; set; }
        }


    }
}
