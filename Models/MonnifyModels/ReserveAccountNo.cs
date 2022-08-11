using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class ReserveAccountNo
    {
        public string accountReference { get; set; }
        public string accountName { get; set; }
        public string currencyCode { get; set; }
        public string contractCode { get; set; }
        public string customerEmail { get; set; }
        public string bvn { get; set; }
        public string customerName { get; set; }
        public bool getAllAvailableBanks { get; set; } = false;
        public List<string> preferredBanks { get; set; }
    }
}
