using PaymentWrapper.Services.Models.Paystack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Paystack
{
   public  interface IPaystack
    {
        Task<VerifyResponse> VerifyPaystackPayment(VerifyRequest request, PaystackCredentials credentials);
        Task<InitializeResponse> InitializePaystackPayment(InitializeRequest request, PaystackCredentials credentials);
    }
}
