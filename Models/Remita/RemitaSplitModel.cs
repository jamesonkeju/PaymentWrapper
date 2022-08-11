using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.Remita
{
   public  class RemitaSplitModel
    {

        public string serviceTypeId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }
        public List<LineItem> lineItems { get; set; }

        public class LineItem
        {
            public string lineItemsId { get; set; }
            public string beneficiaryName { get; set; }
            public string beneficiaryAccount { get; set; }
            public string bankCode { get; set; }
            public string beneficiaryAmount { get; set; }
            public string deductFeeFrom { get; set; }
        }

       
    }

    public class RemitaQueryResponseModel
    {
        public string message { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string merchantId { get; set; }
        public string transactiontime { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string paymentDate { get; set; }



    }
    public class PaymentModel 
    {
        public string serviceTypeId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }

    }
}
