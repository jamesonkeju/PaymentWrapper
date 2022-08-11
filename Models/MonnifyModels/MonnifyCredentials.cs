using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.MonnifyModels
{
    public class MonnifyCredentials
    {
        public string Monnify_Key { get; set; }
        public string Monnify_SercetKey { get; set; }
        public string Monnify_ReturnUrl { get; set; }
        public string Monnify_subAccountCode { get; set; }
        public string Monnify_Contract { get; set; }
        public bool ApplySplit { get; set; }
        public int SplitPecentage { get; set; }
        public bool User_Production_Credentails { get; set; } = false;
    }
}
