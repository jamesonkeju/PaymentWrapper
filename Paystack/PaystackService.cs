using Newtonsoft.Json;
using PaymentWrapper.Services.Models.Paystack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static PaymentWrapper.Services.Models.MonnifyModels.InitializeTransaction;

namespace PaymentWrapper.Services.Paystack
{
    public class PaystackService : IPaystack
    {

        public async Task<Models.Paystack.InitializeResponse> InitializePaystackPayment(InitializeRequest request, PaystackCredentials credentials)
        {
            string baseurl = "";
            string PostTransaction_Url = "";
       

            if (Appsetting.Default.User_Production_Credentails == true)
            {
                baseurl = Appsetting.Default.Paystack_BaseUrl_Production;
                PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Production;
               
            }
            else
            {
                baseurl = Appsetting.Default.Paystack_BaseUrl_Test;
                PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Test;
   
            }
           

            var msg = new Models.Paystack.InitializeResponse();
            try
            {
                var SecKey = string.Format("Bearer {0}", credentials.SecretKey);

                var baseAddress = baseurl + PostTransaction_Url;



                var payload = new InitializeRequestPayload
                {
                    amount = request.amount,
                    callback_url = credentials.callback_url,
                    email = request.email,
                    reference = request.reference,
                    transaction_charge = 0,
                    Key = credentials.PublicKey

                };


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Headers.Add("Authorization", SecKey);
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = JsonConvert.SerializeObject(payload);
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
                msg = JsonConvert.DeserializeObject<Models.Paystack.InitializeResponse>(content);

                return msg;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
                return msg;
            }
        }
        public async Task<VerifyResponse> VerifyPaystackPayment(VerifyRequest request, PaystackCredentials credentials)
        {

            string baseurl = "";
            string PostTransaction_Url = "";
            string VerifyTransaction_Url = "";
           
            if (Appsetting.Default.User_Production_Credentails == true)
            {
                baseurl = Appsetting.Default.Paystack_BaseUrl_Production;
                PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Production;
                VerifyTransaction_Url = Appsetting.Default.Paystack_VerifyTransaction_Url_Production;
            }
            else
            {
                baseurl = Appsetting.Default.Paystack_BaseUrl_Test;
                PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Test;
                VerifyTransaction_Url = Appsetting.Default.Paystack_VerifyTransaction_Url_Test;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var msg = new VerifyResponse();

            try
            {


                var baseAddress = baseurl + VerifyTransaction_Url;
                var url = string.Format(baseAddress, request.reference);

                var myUri = new Uri(url);
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;


                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + credentials.SecretKey);
                myHttpWebRequest.Accept = "application/x-www-form-urlencoded";

                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();


                var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                var json = myStreamReader.ReadToEnd();

                responseStream.Close();
                myWebResponse.Close();

                msg = JsonConvert.DeserializeObject<VerifyResponse>(json);
                return msg;

            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
                return msg;
            }

        }
    }
}
