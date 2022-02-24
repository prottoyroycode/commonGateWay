using AutoMapper;
using Bkash_Service_API.Data;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse;
using Bkash_Service_API.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BkashRecurringPaymentService
{
    public class BkashRecurringService : IBkashRecurringService, IDisposable
    {
        private bool _disposed;
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BkashRecurringService> _logger;
        public BkashRecurringService(DataContext context, IMapper mapper, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, ILogger<BkashRecurringService> logger)
        {
            _context = context;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        private string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private async Task<ServiceResponse<CreateSubscriptionInitiateBkash>> StoreIntitiateSubData(CreateSubscriptionInitiateBkash model)
        {
            var response = new ServiceResponse<CreateSubscriptionInitiateBkash>();
            try
            {
                var storeData = await _context.CreateSubscriptionInitiateBkashes.AddAsync(model);
                var isStored = await _context.SaveChangesAsync();
                if (isStored > 0)
                    response.Message = "success";
                _logger.LogInformation("payment has been initiated ");
                response.Status = true;
                response.Data = model;
                response.StatusCode = ResStatusCode.Success;
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
                response.Message = "failed";
                response.Status = false;
                response.Data = model;
                response.StatusCode = ResStatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<ServiceResponse<CreateSubscriptionResponseDto>> BkashRecurringCreateSubAsync(CreateSubscriptionInitiateBkashDto model)
        {
            var client_Id = GetUserId();
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");
            var random = new Random();
            int num = random.Next(1000);
            var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var response = new ServiceResponse<CreateSubscriptionResponseDto>
            {
                Status = false,
                StatusCode = ResStatusCode.InternalServerError,
                TotalRecords = 0,
                Message = "please try again"
            };

            var mapResponse = _mapper.Map<CreateSubscriptionInitiateBkash>(model);
            mapResponse.redirectUrl = "https://partnersintregation.techapi24.com/api/BkashRedirect";
            mapResponse.startDate = today;
            if (model.freequency == "DAILY")
            {
                mapResponse.expiryDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
            if (model.freequency == "WEEKLY")
            {
                mapResponse.expiryDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            }
            if (model.freequency == "MONTHLY")
            {
                mapResponse.expiryDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            }

            mapResponse.frequency = model.freequency;
            mapResponse.subscriptionRequestId = $"Merchant02-{num}";
            mapResponse.subscriptionReference = $"bkash_ref-{mapResponse.frequency}";
            mapResponse.UserId = model.userId;
            mapResponse.client_UserID = client_Id;

            var storeInitiateData = await StoreIntitiateSubData(mapResponse);
            if (storeInitiateData.Status == false)
            {
                response.Message = "please try again";
                response.Status = false;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Data = null;
            }
            if (storeInitiateData.Status == true)
            {
                try
                {

                    var url = "https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscription";

                    string jsonString = JsonConvert.SerializeObject(mapResponse);
                    var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    var client = _clientFactory.CreateClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("version", "v1.0");
                    client.DefaultRequestHeaders.Add("channelId", "Customer APP");
                    client.DefaultRequestHeaders.Add("timeStamp", "2022-02-10T09:16:28.603855Z");
                    client.DefaultRequestHeaders.Add("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                    //  client.DefaultRequestHeaders.Add("Content-Type ", "application/json");
                    HttpResponseMessage responseHttp = await client.PostAsync(url, payload);
                    string responseJson = await responseHttp.Content.ReadAsStringAsync();
                    if (responseHttp.IsSuccessStatusCode)
                    {

                        var successResponse = JsonConvert.DeserializeObject<CreateSubscriptionResponse>(responseJson);
                        successResponse.RequestID = storeInitiateData.Data.Id;

                        // successResponse.reference = successResponse.redirectURL[^8..];
                        successResponse.reference = successResponse.redirectURL.Split("/").Last();
                        successResponse.client_RedirectURL = model.client_RedirectURL;
                        var storeResponse = await _context.CreateSubscriptionResponses.AddAsync(successResponse);
                        var isStored = await _context.SaveChangesAsync();
                        var resMap = new CreateSubscriptionResponseDto
                        {
                            subscriptionRequestId = successResponse.subscriptionRequestId,
                            redirectURL = successResponse.redirectURL,
                            expirationTime = successResponse.expirationTime,
                            timeStamp = successResponse.timeStamp
                        };
                        if (isStored > 0)
                        {
                            response.Status = true;
                            response.Message = "success";
                            response.TotalRecords = 1;
                            response.StatusCode = ResStatusCode.Success;
                            response.Data = resMap;
                        }


                        else
                        {
                            response.Message = "please try again,request failed";
                            response.Data = null;
                        }


                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;

                }

            }
            return response;
        }

        public async Task<ServiceResponse<RecurringWebhookNotificationDto>> BkashWebhookNotificationAsync(RecurringWebhookNotificationDto recurringWebhookNotificationDto)
        {
            var response = new ServiceResponse<RecurringWebhookNotificationDto>();
            //var userId = await _context.CreateSubscriptionInitiateBkashes.Where(s => s.subscriptionRequestId ==
            //recurringWebhookNotificationDto.subscriptionRequestId).OrderByDescending(o=>o.CreatedDate ).Select(p =>new { 
            //userId =p.UserId,
            //subsscriptionReqId = p.subscriptionRequestId

            //}).FirstOrDefaultAsync();

            var mapResponse = _mapper.Map<RecurringWebhookNotification>(recurringWebhookNotificationDto);
            //   mapResponse.UserId = userId.userId;
            try
            {
                var data = await _context.recurringWebhookNotifications.AddAsync(mapResponse);
                var storeData = await _context.SaveChangesAsync();
                if (storeData > 0)
                    response.Data = recurringWebhookNotificationDto;
                response.Message = "saved successfully";
                response.Status = true;
                response.StatusCode = ResStatusCode.Success;
                response.TotalRecords = 1;
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                response.TotalRecords = 0;
                response.Message = exceptionMessage;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Status = false;
            }
            return response;
        }

        public async Task<ServiceResponse<BkashQuerySubscriptionAndPaymentDto>> BkashQuerySubscriptionAndPaymentAfterCallBackAsync(string subscriptionRequestId)
        {
            var subscriptionRequestIdFind = await _context.CreateSubscriptionResponses.FirstOrDefaultAsync(s => s.reference == subscriptionRequestId);
            var data = subscriptionRequestIdFind.subscriptionRequestId;
            var response = new ServiceResponse<BkashQuerySubscriptionAndPaymentDto>
            {
                Status = false,
                Message = "please try again later",
                StatusCode = ResStatusCode.BadRequest,
                Data = null,
                TotalRecords = 0
            };
            // var client_Id = GetUserId();

            try
            {
                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/request-id/{0}", data);

                DateTime timestamp = DateTime.UtcNow;
                string timestamps = (timestamp.ToString("s") + "Z");

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);


                httpRequest.Headers["version"] = "v1.0";
                httpRequest.Headers["channelId"] = "Merchant WEB";
                httpRequest.Headers["timeStamp"] = "2021-08-24T12:04:31.353163Z";
                httpRequest.Headers["x-api-key"] = "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultData = streamReader.ReadToEnd();
                    var desrializedObject = JsonConvert.DeserializeObject<BkashQuerySubscriptionAndPaymentDto>(resultData);
                    response.Data = desrializedObject;
                    response.Status = true;
                    response.Message = "success";
                    response.TotalRecords = 1;
                    response.StatusCode = ResStatusCode.Success;
                }







                // var client = _clientFactory.CreateClient("");
                // // //var client = new HttpClient();

                // client.DefaultRequestHeaders.Accept.Clear();


                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json;charset=utf8"));
                // client.DefaultRequestHeaders.Add("version", "v1.0");
                // client.DefaultRequestHeaders.Add("channelId", "Merchant WEB");
                // client.DefaultRequestHeaders.Add("timeStamp", timestamps);
                // client.DefaultRequestHeaders.Add("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                // //client.DefaultRequestHeaders.Add("Content-Type", "application/json");


                // var url2 = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/request-id/{0}", subscriptionRequestId);
                // var httpResponse = await client.GetAsync(url2);

                // httpResponse.EnsureSuccessStatusCode();
                // var result = httpResponse.Content.ReadAsStringAsync().Result;
                // if (httpResponse.IsSuccessStatusCode)
                // {
                //     var desrializedObject = JsonConvert.DeserializeObject<BkashQuerySubscriptionAndPaymentDto>(result);
                // }
                // else
                // {
                //     response.Status = false;
                //     response.Message = "we can not precess your request now ,please try again";
                //     response.StatusCode = ResStatusCode.BadRequest;
                //     response.Data = null;
                // }
            }
            catch (Exception ex)
            {
                response.Message = "internal server error";
                response.Status = false;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<ServiceResponse<CancelSubscriptionResponseDto>> CancelSubscriptionAsync(int subscriptionId)
        {



            var serviceResponse = new ServiceResponse<CancelSubscriptionResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");
            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/{0}?reason=TestCancelSubscription", subscriptionId);
                var client = new RestClient();

                var request = new RestRequest(url, Method.Delete);
                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);

                var response = await client.ExecuteAsync(request);
                var desrializedObject = JsonConvert.DeserializeObject<CancelSubscriptionResponseDto>(response.Content);
                var desrializedObjectError = JsonConvert.DeserializeObject<CancelSubErrorResponseDto>(response.Content);
                if (response.IsSuccessful)
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "cancelled";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = desrializedObjectError.reason;
                    serviceResponse.Data = null;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }




                //var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/{0}?reason=TestCancelSubscription", subscriptionId);


                //HttpWebRequest oWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //oWebRequest.PreAuthenticate = true;

                //oWebRequest.Method = "DELETE";
                //oWebRequest.MediaType = "application/json";
                //// Posted forms need to be encoded so change the content type
                //// oWebResponse.ContentType = "application/json; charset=utf-8";
                //oWebRequest.ContentType = "application/json";
                //oWebRequest.Accept = "application/json";
                ////oWebRequest.Headers.Add("Accept", "application/x-www-form-urlencoded");
                //// oWebRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + base64encoded);
                //oWebRequest.Headers.Add("version", "v1.0");
                //oWebRequest.Headers.Add("channelId", "Merchant WEB");
                //oWebRequest.Headers.Add("timeStamp", timestamps);
                //oWebRequest.Headers.Add("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                ////oWebRequest.Headers.Add("Content-Type", "application/json");
                ////byte[] postBytes = Encoding.Default.GetBytes(requestbody);
                //// Convert.FromBase64String(requestbody);
                ////oWebRequest.ContentLength = postBytes.Length;
                //Stream oWebPostStream = oWebRequest.GetRequestStream();
                ////oWebPostStream.Write(postBytes, 0, postBytes.Length);
                //oWebPostStream.Close();
                //oWebPostStream = null;
                //var  oWebResponse = (HttpWebResponse)oWebRequest.GetResponse();
                //if(oWebResponse != null)
                //{
                //    StreamReader oStreamReader = new StreamReader(oWebResponse.GetResponseStream());
                //    var responseJson = oStreamReader.ReadToEnd().Trim().ToString();
                //    var successResponse = JsonConvert.DeserializeObject<CancelSubscriptionResponseDto>(responseJson);

                //    serviceResponse.Status = true;
                //    serviceResponse.Message = "cancelled";
                //    serviceResponse.Data = successResponse;
                //    serviceResponse.StatusCode = ResStatusCode.Success;
                //    serviceResponse.TotalRecords = 1;
                //}



            }
            catch (Exception ex)
            {


                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetPaymentBySubscriptionIdResponseDto>>> GetPaymentListBySubscriptionIdAsync(int subscriptionRequestId)
        {
            var serviceResponse = new ServiceResponse<List<GetPaymentBySubscriptionIdResponseDto>>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");
            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscription/payment/bySubscriptionId/{0}", subscriptionRequestId);
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;
                var desrializedObject = JsonConvert.DeserializeObject<List<GetPaymentBySubscriptionIdResponseDto>>(response.Content);

                if (response.IsSuccessful)
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "some thing went wrong ,please try again";
                    serviceResponse.Data = null;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetPaymentInfoByPaymentIdResponseDto>> GetPaymentInfoByPaymentId(int paymentId)
        {
            var serviceResponse = new ServiceResponse<GetPaymentInfoByPaymentIdResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");
            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscription/payment/{0}", paymentId);
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;
                var desrializedObject = JsonConvert.DeserializeObject<GetPaymentInfoByPaymentIdResponseDto>(response.Content);

                if (response.IsSuccessful)
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "some thing went wrong ,please try again";
                    serviceResponse.Data = null;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<RefundErrorResponseDto>> RefundPaymentAsync(string paymentId, string amount)
        {
            var serviceResponse = new ServiceResponse<RefundErrorResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");
            try
            {

                var url = "https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscription/payment/refund";
                var client = new RestClient();
                var request = new RestRequest(url, Method.Post);
                request.AddBody(new { paymentId = paymentId, amount = amount });
                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;

                var desrializedObject = JsonConvert.DeserializeObject<RefundErrorResponseDto>(response.Content);

                if (response.IsSuccessful)
                {

                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = desrializedObject.reason;
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

        }

        public async Task<ServiceResponse<QueryBySubscriptionIdResponseDto>> QueryBySubscriptionId(int subscriptionID)
        {
            var serviceResponse = new ServiceResponse<QueryBySubscriptionIdResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");

            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/{0}", subscriptionID);
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);

                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;

                var desrializedObject = JsonConvert.DeserializeObject<QueryBySubscriptionIdResponseDto>(response.Content);

                if (response.IsSuccessful)
                {

                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "failed";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;


        }

        public async Task<ServiceResponse<SubscriptionListResponseDto>> SubscriptionList(int pageNumber, int size)
        {
            var serviceResponse = new ServiceResponse<SubscriptionListResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");

            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscriptions/{0}/{1}", pageNumber, size);
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);

                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;
                var desrializedObject = JsonConvert.DeserializeObject<SubscriptionListResponseDto>(response.Content);

                if (response.IsSuccessful)
                {

                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = 1;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "failed";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetScheduleResponseDto>> GetPaymentScheduleUrl(string freequency, string startDate, string endDate)
        {
            var serviceResponse = new ServiceResponse<GetScheduleResponseDto>
            {
                Status = false,
                Message = "sorry request can not proceed ,please try again",
                TotalRecords = 0,
                Data = null,
                StatusCode = ResStatusCode.BadRequest
            };
            DateTime timestamp = DateTime.UtcNow;
            string timestamps = (timestamp.ToString("s") + "Z");

            try
            {

                var url = string.Format("https://gateway.sbrecurring.pay.bka.sh/gateway/api/subscription/payment/schedule?frequency={0}&startDate={1}&expiryDate={2}", freequency, startDate, endDate);
                var client = new RestClient();
                var request = new RestRequest(url, Method.Get);

                request.AddHeader("version", "v1.0");
                request.AddHeader("channelId", "Merchant WEB");
                request.AddHeader("timeStamp", timestamps);
                request.AddHeader("x-api-key", "27vsY1D_INTqSRbiSGEXb8HRtA1j3grr");
                request.AddHeader("Content-Type", "application/json");
                //await client.DeleteAsync(request);
                //var resRest = client.GetAsync(request);
                var response = await client.ExecuteAsync(request);
                var data = response.Content;
                var desrializedObject = JsonConvert.DeserializeObject<GetScheduleResponseDto>(response.Content);

                if (response.IsSuccessful)
                {

                    serviceResponse.Status = true;
                    serviceResponse.Message = "success";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.TotalRecords = desrializedObject.count;
                }
                else
                {
                    serviceResponse.Status = true;
                    serviceResponse.Message = "failed";
                    serviceResponse.Data = desrializedObject;
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    serviceResponse.TotalRecords = 0;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Message = ex.Message;
                // serviceResponse.Data = successResponse;
                serviceResponse.StatusCode = ResStatusCode.BadRequest;
                serviceResponse.TotalRecords = 0;
            }
            return serviceResponse;

        }

        public async Task<bool> CheckIfUserExists(string userId)
        {


            //  remaining = EF.Functions.DateDiffDay(DateTime.Parse(r.nextPaymentDate), DateTime.Parse(r.dueDate))
            var query = await (from c in _context.CreateSubscriptionInitiateBkashes
                               join r in _context.recurringWebhookNotifications
                           on c.subscriptionRequestId equals r.subscriptionRequestId
                               where c.UserId == userId && (c.frequency == "WEEKLY" || c.frequency == "DAILY" || c.frequency == "MONTHLY")
                               orderby r.CreatedDate descending
                               select new
                               {
                                   user = c.UserId,
                                   reqId = r.subscriptionRequestId,
                                   subId = r.subscriptionId,
                                   dueData = r.dueDate,
                                   nextDate = r.nextPaymentDate,
                                   freequency = c.frequency,
                                   status = r.subscriptionStatus

                               }
            ).FirstOrDefaultAsync();
            if (query.status == "CANCELLED")
            {
                return true;
            }
            var dueDate = Convert.ToDateTime(query.dueData);
            var nextDate = Convert.ToDateTime(query.nextDate);
            var dif = nextDate - dueDate;
            var days = dif.Days;
            if (days > 0)
            {
                return true;
            }

            //var data = await _context.CreateSubscriptionInitiateBkashes.FirstOrDefaultAsync(s => s.UserId == userId);
            //if (data != null)
            //    return true;
            return false;



        }

        public async Task<ServiceResponse<UserSubscriptionStatusDto>> CheckUsersSubscriptionStatus(string userId)
        {
            var response = new ServiceResponse<UserSubscriptionStatusDto>()
            {

                Data = null,
                Message = "please try again",
                Status = false,
                TotalRecords = 0,
                StatusCode = ResStatusCode.BadRequest
            };
            try
            {
                var query = await (from c in _context.CreateSubscriptionInitiateBkashes
                                   join r in _context.recurringWebhookNotifications
                               on c.subscriptionRequestId equals r.subscriptionRequestId
                                   where c.UserId == userId && (c.frequency == "WEEKLY" || c.frequency == "DAILY" || c.frequency == "MONTHLY")
                                   orderby r.CreatedDate descending
                                   select new UserSubscriptionStatusDto
                                   {
                                       userId = c.UserId,
                                       subscriptionRequestId = r.subscriptionRequestId,
                                       subscriptionId = r.subscriptionId,
                                       dueDate = r.dueDate,
                                       nextPaymentDate = r.nextPaymentDate,
                                       frequency = c.frequency,
                                       subscriptionStatus = r.subscriptionStatus

                                   }
            ).FirstOrDefaultAsync();
                if(query.subscriptionStatus ==null)
                {
                    query.subscriptionStatus = "Active";
                }
                if (query != null)
                {
                    response.Data = query;
                    response.Message = "succeed";
                    response.TotalRecords = 1;
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    return response;
                }
                if (query == null)
                {
                    response.Data = null;
                    response.Message = "no data found";
                    response.TotalRecords = 0;
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    return response;
                }
                return response;



            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = ex.Message;
                response.TotalRecords = 0;
                response.Status = false;
                response.StatusCode = ResStatusCode.InternalServerError;
                return response;
            }



        }
    }
}

