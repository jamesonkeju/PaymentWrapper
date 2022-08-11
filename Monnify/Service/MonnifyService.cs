
using Newtonsoft.Json;
using PaymentWrapper.Services.Models.MonnifyModels;
using PaymentWrapper.Services.Monnify.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static PaymentWrapper.Services.Models.MonnifyModels.AccessTokenResponse;
using static PaymentWrapper.Services.Models.MonnifyModels.InitializeTransaction;

namespace PaymentWrapper.Services.Monnify.Service
{
    public class MonnifyService : IMonnify
    {
        private async Task<AccessTokenRoot> GetAccessToken(string ApiKey, string SecretKey, string baseurl, string Auth_Url)
        {
            var result = new AccessTokenResponse.AccessTokenRoot();

            try
            {
                var payload = new AccessTokenRequest
                {
                    ApiKey = ApiKey,
                    SceretKey = SecretKey,

                };

                var content = Convert.ToBase64String(Encoding.UTF8.GetBytes(ApiKey + ":" + SecretKey));

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new HttpClient();
                string buildurl = baseurl + Auth_Url;

                using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
                {
                    var json = JsonConvert.SerializeObject(content);

                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + content);

                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        request.Content = stringContent;

                        using (var response = await client
                                           .PostAsync(buildurl, stringContent)
                                           .ConfigureAwait(false))
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<AccessTokenResponse.AccessTokenRoot>(data);
                            return result;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                result.requestSuccessful = false;
                result.responseMessage = ex.Message;
                return result;
            }
        }

        public async Task<MonnifyRequeryResponse.Root> VerifyMonnifyPayment(string referanceNo, MonnifyCredentials credentails)
        {
            try
            {
                string baseurl = "";
                string PostTransaction_Url = "";
                string Monnify_Auth_URL = "";
                string Monnify_Currency = "";
                string Monnify_Requery = "";



                if (Appsetting.Default.User_Production_Credentails == true)
                {
                    baseurl = Appsetting.Default.Monnify_BaseUrl_Production;
                    PostTransaction_Url = Appsetting.Default.Monnify_NewTransaction_Production;
                    Monnify_Auth_URL = Appsetting.Default.Monnify_Auth_Production;
                    Monnify_Requery = Appsetting.Default.Monnify_Requery_Production;

                }
                else
                {
                    baseurl = Appsetting.Default.Monnify_BaseUrl_Test;
                    PostTransaction_Url = Appsetting.Default.Monnify_NewTransaction_Test;
                    Monnify_Auth_URL = Appsetting.Default.Monnify_Auth_Test;
                    Monnify_Requery = Appsetting.Default.Monnify_Requery_Test;
                }

                Monnify_Currency = Appsetting.Default.Monnify_Current;

                string url = HttpUtility.UrlEncode(referanceNo);
                var gettoken = await GetAccessToken(credentails.Monnify_Key, credentails.Monnify_SercetKey, baseurl, Monnify_Auth_URL);
                string statusurl = baseurl + Monnify_Requery + url;
                var jsondata = GetMonnifyAsync(statusurl, gettoken.responseBody.accessToken);
                var result = JsonConvert.DeserializeObject<MonnifyRequeryResponse.Root>(jsondata.Result);

                return result;
            }
            catch (Exception ex)
            {

                return new MonnifyRequeryResponse.Root()
                {
                    requestSuccessful = false,
                    responseMessage = ex.Message
                };
            }
        }

        public async Task<InitializeResponse> InitializeMonnifyPayment(InitializeNewPayment Payload, MonnifyCredentials credentails)
        {

            string baseurl = "";
            string PostTransaction_Url = "";
            string Monnify_Auth_URL = "";
            string Monnify_Currency = "";
            if (Appsetting.Default.User_Production_Credentails == true)
            {
             
                baseurl = Appsetting.Default.Monnify_BaseUrl_Production;
                PostTransaction_Url = Appsetting.Default.Monnify_NewTransaction_Production;
                Monnify_Auth_URL = Appsetting.Default.Monnify_Auth_Production;
            }
            else
            {
                baseurl = Appsetting.Default.Monnify_BaseUrl_Test;
                PostTransaction_Url = Appsetting.Default.Monnify_NewTransaction_Test;
                Monnify_Auth_URL = Appsetting.Default.Monnify_Auth_Test;
            }
            Monnify_Currency = Appsetting.Default.Monnify_Current;

            try
            {


                if (credentails.ApplySplit == true)
                {
                    var paymentMode = new List<string>
                    {
                        "CARD",
                        "ACCOUNT_TRANSFER"
                    };

                    var splitHolder = new List<IncomeSplitConfig>();

                    var prepareIncome = new IncomeSplitConfig
                    {
                        subAccountCode = credentails.Monnify_subAccountCode,
                        feeBearer = true,
                        feePercentage = credentails.SplitPecentage,
                        splitAmount = Convert.ToInt32(Payload.AmountToSend),


                    };
                    splitHolder.Add(prepareIncome);

                    var metaTag = new Metadata
                    {
                        name = Payload.customerName
                    };

                    var prepareData = new InitializeSplit
                    {
                        amount = Convert.ToDouble(Payload.AmountToSend),
                        currencyCode = Monnify_Currency,
                        customerEmail = Payload.CustomerEmail,
                        customerName = Payload.customerName,
                        paymentDescription = Payload.PaymentDescription,
                        paymentMethods = paymentMode,
                        paymentReference = Payload.PaymentReference,
                        redirectUrl = credentails.Monnify_ReturnUrl,
                        incomeSplitConfig = splitHolder,
                        metadata = metaTag,
                        contractCode = credentails.Monnify_Contract
                    };

                    string processpayment = baseurl + PostTransaction_Url;

                    var gettoken = await GetAccessToken(credentails.Monnify_Key, credentails.Monnify_SercetKey, baseurl, Monnify_Auth_URL);


                    var json = JsonConvert.SerializeObject(prepareData);

                    if (gettoken.requestSuccessful == false)

                    {

                        return new InitializeResponse()
                        {
                            requestSuccessful = false,
                            responseMessage = "Unable to generate access token at the moment. Please try again later"
                        };
                    }


                    string accessToken = gettoken.responseBody.accessToken;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var responseData = await PostMonnifyAsync(prepareData, accessToken, processpayment);

                    var result = JsonConvert.DeserializeObject<InitializeResponse>(responseData);
                    return result;

                }
                else
                {
                    var paymentMode = new List<string>();

                    paymentMode.Add("CARD");
                    paymentMode.Add("ACCOUNT_TRANSFER");



                    var prepareData = new SinglePayment
                    {
                        amount = Convert.ToDouble(Payload.AmountToSend),
                        currencyCode = Monnify_Currency,
                        customerEmail = Payload.CustomerEmail,
                        customerName = Payload.customerName,
                        paymentDescription = Payload.PaymentDescription,
                        paymentMethods = paymentMode,
                        paymentReference = Payload.PaymentReference,
                        redirectUrl = credentails.Monnify_ReturnUrl,
                        contractCode = credentails.Monnify_Contract
                    };

                    string processpayment = baseurl + PostTransaction_Url;

                    var gettoken = await GetAccessToken(credentails.Monnify_Key, credentails.Monnify_SercetKey,
                        baseurl, Monnify_Auth_URL);


                    var json = JsonConvert.SerializeObject(prepareData);

                    if (gettoken.requestSuccessful == false)

                    {

                        return new InitializeResponse()
                        {
                            requestSuccessful = false,
                            responseMessage = "Unable to generate access token at the moment. Please try again later"
                        };
                    }


                    string accessToken = gettoken.responseBody.accessToken;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var responseData = await PostMonnifyAsync(prepareData, accessToken, processpayment);

                    var result = JsonConvert.DeserializeObject<InitializeResponse>(responseData);

                    return result;
                }

            }
            catch (Exception ex)
            {
                return new InitializeResponse()
                {
                    requestSuccessful = false,
                    responseMessage = ex.Message
                };
            }
        }


        private async Task<string> PostMonnifyAsync(object content, string token, string apiurl)
        {
            string buildurl = apiurl;


            var client = new HttpClient();


            using (var request = new HttpRequestMessage(HttpMethod.Post, buildurl))
            {
                var json = JsonConvert.SerializeObject(content);

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client
                                       .PostAsync(buildurl, stringContent)
                                       .ConfigureAwait(false))
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        return data;
                    }

                }
            }
        }


        public static async Task<string> GetMonnifyAsync(string buildurl, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(buildurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    using (var response = await client.GetAsync(buildurl).ConfigureAwait(false))
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }

        public async Task<ReserveAccountResponse.Root> ReserveAccountMonnify(ReserveAccountNo payload, MonnifyCredentials credentails)
        {
            var msg = new ReserveAccountResponse.Root();

            string baseurl = "";
            string Monnify_Currency = "";
            if (Appsetting.Default.User_Production_Credentails == true)
            {

                baseurl = Appsetting.Default.Monnify_BaseUrl_Production;
              
            }
            else
            {
                baseurl = Appsetting.Default.Monnify_BaseUrl_Test;
              
            }
            Monnify_Currency = Appsetting.Default.Monnify_Current;

            try
            {

                    string processpayment = baseurl + Appsetting.Default.Monnify_Reserve_AccountNo; ;

                    var gettoken = await GetAccessToken(credentails.Monnify_Key, credentails.Monnify_SercetKey, baseurl, Appsetting.Default.Monnify_Auth_Production);

                    if (gettoken.requestSuccessful == false)

                    {
                        return new ReserveAccountResponse.Root()
                        {
                            requestSuccessful = false,
                            responseMessage = "Unable to generate access token at the moment. Please try again later"
                        };
                    }


                    string accessToken = gettoken.responseBody.accessToken;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var responseData = await PostMonnifyAsync(payload, accessToken, processpayment);

                    msg = JsonConvert.DeserializeObject<ReserveAccountResponse.Root>(responseData);
                    return msg;
            }
            catch (Exception ex)
            {
                return new ReserveAccountResponse.Root()
                {
                    requestSuccessful = false,
                    responseMessage = ex.Message
                };
            }
        }
    }
}
