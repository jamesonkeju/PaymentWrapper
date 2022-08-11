using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class MonnifyRequeryResponse
    {

        public class AccountDetails
        {
            public string accountName { get; set; }
            public string accountNumber { get; set; }
            public string bankCode { get; set; }
            public string amountPaid { get; set; }
        }

        public class AccountPayment
        {
            public string accountName { get; set; }
            public string accountNumber { get; set; }
            public string bankCode { get; set; }
            public string amountPaid { get; set; }
        }

        public class Customer
        {
            public string email { get; set; }
            public string name { get; set; }
        }

        public class ResponseBody
        {
            public string transactionReference { get; set; }
            public string paymentReference { get; set; }
            public string amountPaid { get; set; }
            public string totalPayable { get; set; }
            public string settlementAmount { get; set; }
            public string paidOn { get; set; }
            public string paymentStatus { get; set; }
            public string paymentDescription { get; set; }
            public string currency { get; set; }
            public string paymentMethod { get; set; }
            public Product product { get; set; }
            public object cardDetails { get; set; }
            public AccountDetails accountDetails { get; set; }
            public List<AccountPayment> accountPayments { get; set; }
            public Customer customer { get; set; }
            public MetaData metaData { get; set; }
        }


        public class MetaData
        {
            public string name { get; set; }
            public string age { get; set; }
        }

        public class Product
        {
            public string type { get; set; }
            public string reference { get; set; }
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
