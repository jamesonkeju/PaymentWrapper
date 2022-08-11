using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
   public  class InitializeNewPayment
    {
        public decimal AmountToSend { get; set; }
        public decimal TransactionFee { get; set; }
        public string PaymentReference { get; set; }
        public string customerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Phone { get; set; }
        public string PayItemName { get; set; }
        public string PaymentDescription { get; set; }
    }
}
