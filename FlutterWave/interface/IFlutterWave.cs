using PaymentWrapper.Models.FlutterWave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentWrapper.Models.FlutterWave.FlutterSinglePaymentResponse;

namespace PaymentWrapper.Services.FlutterWave
{
    public  interface IFlutterWave
    {
        Task<Root> VerifyFlutterWavePayment(string FlutterTransactionReference, FlutterCredentials credentails);

          Task<FlutterResponse> InitializeFlutterWavePayment(FlutterRequest payload, FlutterCredentials credentails);
    }
}
