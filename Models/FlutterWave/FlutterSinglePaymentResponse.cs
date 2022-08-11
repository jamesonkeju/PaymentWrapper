using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWrapper.Services.Models.FlutterWave
{
    public class FlutterSinglePaymentResponse
    {
        public class FlutterResponse
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public DataObject Data { get; set; }
        }

        public class DataObject
        {
            public string Link { get; set; }
        }
    }
}
