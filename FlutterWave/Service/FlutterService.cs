using Newtonsoft.Json;
using PaymentWrapper.Models.FlutterWave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static PaymentWrapper.Models.FlutterWave.FlutterSinglePaymentResponse;

namespace PaymentWrapper.Services.FlutterWave.Service
{
    public class FlutterService : IFlutterWave
    {
        public  async Task<FlutterResponse> InitializeFlutterWavePayment(FlutterRequest payload, FlutterCredentials credentails)
        {
            var msg = new FlutterResponse();

            try
            {

                string FlutterWavePaymentURL = "";
                if (Appsetting.Default.User_Production_Credentails == true)
                {
                    FlutterWavePaymentURL = Appsetting.Default.Fluttter_FlutterWavePaymentURL_Production;

                }
                else
                {
                    FlutterWavePaymentURL = Appsetting.Default.Fluttter_FlutterWavePaymentURL_Test;

                }

                var SecKey = string.Format("Bearer {0}", credentails.Flutter_SercetKey);

                var baseAddress = FlutterWavePaymentURL;

                string parsedContent = JsonConvert.SerializeObject(payload);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Headers.Add("Authorization", SecKey);
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

            
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                var cc = JsonConvert.DeserializeObject(content);
                var cd = JsonConvert.DeserializeObject(cc.ToString());

                msg = JsonConvert.DeserializeObject<FlutterResponse>(content);

                return msg;
            }
            catch (Exception ex)
            {
                msg.Status = "Error";
                msg.Message = ex.Message;
                return msg;
            }
        }

        public async Task<Root> VerifyFlutterWavePayment(string referanceNo, FlutterCredentials credentials)
        {
            var msg = new Root();
            HttpClient client = new HttpClient();
            try
            {

                string baseurl = "";
                string FlutterRequeryURL = "";

                if (Appsetting.Default.User_Production_Credentails == true)
                {
                    FlutterRequeryURL = Appsetting.Default.Fluttter_FlutterRequeryURL_Production;
                
                }
                else
                {
                    FlutterRequeryURL = Appsetting.Default.Fluttter_FlutterRequeryURL_Test;

                }


                string _url = FlutterRequeryURL.Replace("{0}", referanceNo);
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credentials.Flutter_SercetKey);

                var data = await client.GetAsync(_url);

                Task<string> json = data.Content.ReadAsStringAsync();
                string resultValue = json.Result;
                msg = JsonConvert.DeserializeObject<Root>(resultValue);
                return msg;

            }
            catch (Exception ex)
            {
                msg.status = "false";
                msg.message = ex.Message;
                return msg;
            }
        }
    }
}
