using PaymentWrapper.Models.Paystack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Paystack
{
   public  interface IPaystack
    {
        Task<PaymentWrapper.Models.Paystack.VerifyResponse> VerifyPaystackPayment(VerifyRequest request, PaystackCredentials credentials);
        Task<PaymentWrapper.Models.Paystack.InitializeResponse> InitializePaystackPayment(InitializeRequest request, PaystackCredentials credentials);
    }
}
