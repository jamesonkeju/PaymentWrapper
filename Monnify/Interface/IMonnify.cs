using PaymentWrapper.Services.Models.MonnifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentWrapper.Services.Models.MonnifyModels.InitializeTransaction;

namespace PaymentWrapper.Services.Monnify.Interface
{
    public interface IMonnify
    {
        Task<InitializeResponse> InitializeMonnifyPayment(InitializeNewPayment Payload, MonnifyCredentials credentails);
        Task<MonnifyRequeryResponse.Root> VerifyMonnifyPayment(string referanceNo, MonnifyCredentials credentails);

        Task<ReserveAccountResponse.Root> ReserveAccountMonnify(ReserveAccountNo payload, MonnifyCredentials credentails);

    }
}
