using PaymentWrapper.Models.Remita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentWrapper.Models.Remita.RemitaSplitModel;

namespace PaymentWrapper.Services.Remita
{
    public interface IRemita
    {
        Task<RemitaQueryResponseModel> VerifyRemitaPayment(RequeryRemitaModel payload, RemitaCredentials credentials);
        Task<Generated_RRR_Response> InitiateRemitaSinglePayment(PaymentModel Payload, RemitaCredentials credentials);
        Task<Generated_RRR_Response> InitiateRemitaSplitPayment(RemitaSplitModel SplitPayload, RemitaCredentials credentials);
    }
}
