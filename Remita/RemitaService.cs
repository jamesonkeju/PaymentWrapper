using Newtonsoft.Json;

using PaymentWrapper.Services.Models.Remita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace PaymentWrapper.Services.Remita
{
    public class RemitaService : IRemita
    {

        private string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }
        public async Task<RemitaQueryResponseModel> VerifyRemitaPayment(RequeryRemitaModel payload, RemitaCredentials credentials)
        {
            var msg = new RemitaQueryResponseModel();
            try
            {

                string paymentinit_url = "";
                string PostTransaction_Url = "";
                string VerifyTransaction_Url = "";
                string Check_Status_Url = "";

                if (Appsetting.Default.User_Production_Credentails == true)
                {
                    paymentinit_url = Appsetting.Default.Paystack_BaseUrl_Production;
                    PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Production;
                    VerifyTransaction_Url = Appsetting.Default.Paystack_VerifyTransaction_Url_Production;
                    Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Production;
                }
                else
                {
                    paymentinit_url = Appsetting.Default.Paystack_BaseUrl_Test;
                    PostTransaction_Url = Appsetting.Default.Paystack_InitializeTransaction_Url_Test;
                    VerifyTransaction_Url = Appsetting.Default.Paystack_VerifyTransaction_Url_Test;
                    Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Test;
                }

                var hash = SHA512(payload.OrderId + credentials.apiKey + credentials.merchantId);
                WebClient webClient = new WebClient
                {
                    UseDefaultCredentials = true,
                    Headers =
                        {
                            ["Authorization"] = $"remitaConsumerKey={credentials.merchantId},remitaConsumerToken={hash}",

                        }
                };


                try
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                      System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                      System.Security.Cryptography.X509Certificates.X509Chain chain,
                      System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;


                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    // Pass the handler to httpclient(from you are calling api)
                    HttpClient client = new HttpClient(clientHandler);

                    var remitaResponseStr = (Check_Status_Url + "/" + credentials.merchantId + "/" + payload.OrderId + "/" + hash + "/" + "orderstatus.reg");

                    // var URL = _configuration.GetValue<string>("RemitaRRURL");

                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var remitaResponsek = webClient.UploadString(new Uri(remitaResponseStr), "POST");

                    string jsondata = "";
                    jsondata = remitaResponsek.Replace("jsonp (", "");
                    jsondata = jsondata.Replace(")", "");
                    jsondata = jsondata.Replace("jsonp", "");


                    msg = JsonConvert.DeserializeObject<RemitaQueryResponseModel>(jsondata);

                    return msg;

                }
                catch (Exception ex)
                {
                    msg.message = ex.Message;
                    msg.status = "Error";
                    return msg;
                }
            }
            catch (Exception ex)
            {

                msg.message = ex.Message;
                msg.status = "Error";
                return msg;
            }
        }
        public async Task<Generated_RRR_Response> InitiateRemitaSplitPayment(RemitaSplitModel SplitPayload, RemitaCredentials credentials)
        {
            var msg = new Generated_RRR_Response();
            try
            {

                string paymentinit_url = "";    
                string Check_Status_Url = "";

                if (credentials.User_Production_Credentails == true)
                {
                    paymentinit_url = Appsetting.Default.Remita_paymentinit_url_Production;
                    Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Production;
                }
                else
                {
                    paymentinit_url = Appsetting.Default.Remita_paymentinit_url_Test;
                    Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Test;
                }


                var hash = SHA512(credentials.merchantId + credentials.serviceTypeId + SplitPayload.orderId + SplitPayload.amount + credentials.apiKey);
                WebClient webClient = new WebClient
                {
                    UseDefaultCredentials = true,
                    Headers =
                        {
                            ["Authorization"] = $"remitaConsumerKey={credentials.merchantId},remitaConsumerToken={hash}",

                        }
                };
                try
                {
                    var ttry = JsonConvert.SerializeObject(SplitPayload);


                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var remitaResponse = webClient.UploadString(new Uri(paymentinit_url), "POST", ttry);

                    string jsondata = "";
                    jsondata = remitaResponse.Replace("jsonp (", "");
                    jsondata = jsondata.Replace(")", "");
                    jsondata = jsondata.Replace("jsonp", "");


                    msg = JsonConvert.DeserializeObject<Generated_RRR_Response>(jsondata);

                    return msg;

                }
                catch (Exception ex)
                {
                    msg.status = "Error";
                    return msg;
                }
            }
            catch (Exception ex)
            {

     
                msg.status = "Error";
                return msg;
            }
        }
        public async Task<Generated_RRR_Response> InitiateRemitaSinglePayment(PaymentModel SplitPayload, RemitaCredentials credentials)
        {
            string paymentinit_url = "";
            string Check_Status_Url = "";

            if (credentials.User_Production_Credentails == true)
            {
                paymentinit_url = Appsetting.Default.Remita_paymentinit_url_Production;
                Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Production;
            }
            else
            {
                paymentinit_url = Appsetting.Default.Remita_paymentinit_url_Test;
                Check_Status_Url = Appsetting.Default.Remita_Check_Status_Url_Test;
            }


            var msg = new Generated_RRR_Response();
            try
            {
                var hash = SHA512(credentials.merchantId + credentials.serviceTypeId + SplitPayload.orderId + SplitPayload.amount + credentials.apiKey);
                WebClient webClient = new WebClient
                {
                    UseDefaultCredentials = true,
                    Headers =
                        {
                            ["Authorization"] = $"remitaConsumerKey={credentials.merchantId},remitaConsumerToken={hash}",

                        }
                };
                try
                {
                    var ttry = JsonConvert.SerializeObject(SplitPayload);


                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var remitaResponse = webClient.UploadString(new Uri(paymentinit_url), "POST", ttry);

                    string jsondata = "";
                    jsondata = remitaResponse.Replace("jsonp (", "");
                    jsondata = jsondata.Replace(")", "");
                    jsondata = jsondata.Replace("jsonp", "");


                    msg = JsonConvert.DeserializeObject<Generated_RRR_Response>(jsondata);

                    return msg;

                }
                catch (Exception ex)
                {
                 
                    msg.status = "Error";
                    return msg;
                }
            }
            catch (Exception ex)
            {
                msg.status = "Error";
                return msg;
            }
        }
    }
}