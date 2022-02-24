using AutoMapper;
using Bkash_Service_API.Data;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities.Bkash;
using Bkash_Service_API.Models.Entities.BkashRequestResponse;
using Bkash_Service_API.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BkashService
{
    public class BkashService : IBkashService
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        private BkashTokenResponse bKashToken;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BkashService(DataContext context, IMapper mapper, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            bKashToken = GenerateToken();
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;

        }
        private string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<ServiceResponse<BkashAgreementStatusVM>> CheckAgreementStatusByAgreementId(string agreementID)
        {
            //var url = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/agreement/status";
            var url = AppConstants.CheckAgreementStatusURL;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var Obj = new { agreementID = agreementID };
            var data = JsonConvert.SerializeObject(Obj);

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                var successResponse = JsonConvert.DeserializeObject<BkashAgreementStatusVM>(resultResponse);

                return new ServiceResponse<BkashAgreementStatusVM> { Data = successResponse, Message = "Success", Status = true, StatusCode = ResStatusCode.Success };
            }
            return new ServiceResponse<BkashAgreementStatusVM> { Data = null, Message = "failed", Status = false, StatusCode = ResStatusCode.InternalServerError };
            //else
            //{
            //    var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
            //    var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, agreementID = agreementID };

            //    return new Response { Data = exception, Message = "Failed", Status = false, StatusCode = ResStatusCode.NotFound };
            //}

        }
        public async Task<ServiceResponse<BkashCreateAgreementRequest>> GetReport()
        {

            var response = new ServiceResponse<BkashCreateAgreementRequest>();


            return response;

        }
        public async Task<ServiceResponse<AgreementResponseDto>> CreateAgreement2(string userId, string clientSuccess_redirectURL, string client_invoiceNUmber)
        {
            
            var random = new Random();
            int num = random.Next(1000);

            //var d = await _context.BkashCreateAgreementRequests.Include(s => s.bkashCreateResponse).ThenInclude(p=>p.bkashExecuteAgreementRequest).
            //   ThenInclude(q=>q.bkashExecuteAgreementResponse).ToListAsync();
            // var reverse = await _context.BkashCreateAgreementResponses.Include(s => s.bkashCreateRequest).ToListAsync();
            bKashCreateAgreementResponseVM resultResponse;
            var url = AppConstants.CreateAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var requestData = new BkashCreateAgreementRequest(AppConstants.Mode, AppConstants.PaymentCallback, userId);
            requestData.IsActive = true;
            requestData.UserId = userId;
            requestData.client_UserID = GetUserId();
            requestData.merchantInvoiceNumber = $"invMerchant-{num}";
            requestData.clientSuccess_redirectURL = clientSuccess_redirectURL;
            requestData.client_invoiceNUmber = client_invoiceNUmber;
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var storeRequestDataintoDB = await _context.BkashCreateAgreementRequests.AddAsync(requestData);
                var isReqDataStored = await _context.SaveChangesAsync();

                if (isReqDataStored != 1)
                {
                    return new ServiceResponse<AgreementResponseDto> { Message = "internal server error", Status = false, Data = null, StatusCode = ResStatusCode.InternalServerError };
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = await streamReader.ReadToEndAsync();

                if (result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashCreateAgreementResponse>(result);

                    //  resultData.BkashCreateRequestId = storeRequestDataintoDB.Entity.Id;


                    // int client_ID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    resultData.client_UserID = GetUserId();
                    resultData.merchantInvoiceNumber = requestData.merchantInvoiceNumber;
                    resultData.UserId = requestData.UserId;
                    resultData.clientSuccess_redirectURL = clientSuccess_redirectURL;
                    resultData.client_invoiceNUmber =client_invoiceNUmber;
                    await _context.BkashCreateAgreementResponses.AddAsync(resultData);
                    _ = _context.SaveChangesAsync();
                    var mapResponse = _mapper.Map<AgreementResponseDto>(resultData);
                    return new ServiceResponse<AgreementResponseDto> { Message = "success", TotalRecords = 1, Status = true, Data = mapResponse };
                    // return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }
                else
                {
                    var resultData = JsonConvert.DeserializeObject<BkashCreateAgreementResponse>(result);
                    return new ServiceResponse<AgreementResponseDto> { Message = resultData.statusMessage, Status = false, Data = null };
                    //  return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }



            }
            catch (Exception ex)
            {

                var aaa = ex.ToString();

                throw ex;
            }

        }


        public async Task<bKashCreateAgreementResponseVM> CreateAgreement(string userId, string clientSuccess_redirectURL, string client_invoiceNUmber)
        {
            var random = new Random();
            int num = random.Next(1000);

            //var d = await _context.BkashCreateAgreementRequests.Include(s => s.bkashCreateResponse).ThenInclude(p=>p.bkashExecuteAgreementRequest).
            //   ThenInclude(q=>q.bkashExecuteAgreementResponse).ToListAsync();
            // var reverse = await _context.BkashCreateAgreementResponses.Include(s => s.bkashCreateRequest).ToListAsync();
            bKashCreateAgreementResponseVM resultResponse;
            var url = AppConstants.CreateAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var requestData = new BkashCreateAgreementRequest(AppConstants.Mode, AppConstants.PaymentCallback, AppConstants.PayerReference);
            requestData.IsActive = true;
            requestData.UserId = userId;
            requestData.merchantInvoiceNumber = $"invMerchant-{num}";
            requestData.clientSuccess_redirectURL = clientSuccess_redirectURL;
            requestData.client_invoiceNUmber = client_invoiceNUmber;
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var storeRequestDataintoDB = await _context.BkashCreateAgreementRequests.AddAsync(requestData);
                var isReqDataStored = await _context.SaveChangesAsync();

                if (isReqDataStored != 1)
                {
                    return new bKashCreateAgreementResponseVM { statusMessage = "error" };
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = await streamReader.ReadToEndAsync();

                if (result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashCreateAgreementResponse>(result);
                    //  resultData.BkashCreateRequestId = storeRequestDataintoDB.Entity.Id;


                    resultData.merchantInvoiceNumber = requestData.merchantInvoiceNumber;
                    resultData.UserId = requestData.UserId;
                    await _context.BkashCreateAgreementResponses.AddAsync(resultData);
                    _ = _context.SaveChangesAsync();
                    return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }
                else
                {
                    var resultData = JsonConvert.DeserializeObject<BkashCreateAgreementResponse>(result);
                    return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }



            }
            catch (Exception ex)
            {

                var aaa = ex.ToString();


                throw ex;
            }

        }

        public async Task<ServiceResponse<object>> CreatePaymentWithAgreementId(BkashCreatePaymentRequestVM bkashCreatePaymentRequestVM)
        {
            var client_UserID = GetUserId();
            var response = new ServiceResponse<object>();
            response.Data = null;
            response.Message = "error";
            response.Status = false;
            var bKashToken = GenerateTokenForPaymentWithAgreement();
            bKashCreateAgreementResponseVM resultResponse;
            var url = AppConstants.CreatePaymentWithAgreementIdURL;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var requestData = new BkashCreatePaymentWithAgreementRequest();
            requestData.amount = bkashCreatePaymentRequestVM.amount;
            requestData.agreementID = bkashCreatePaymentRequestVM.agreementId;
            requestData.callbackURL = AppConstants.CreatePaymentCallBackURL;
            requestData.currency = bkashCreatePaymentRequestVM.currency;
            requestData.intent = bkashCreatePaymentRequestVM.intent;
            requestData.merchantInvoiceNumber = "invMerchant-347";
            requestData.mode = bkashCreatePaymentRequestVM.mode;
            requestData.client_UserID = client_UserID;

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var storePaymentRequestDataIntoDb = await _context.BkashCreatePaymentWithAgreementRequests.AddAsync(requestData);
                var ispaymentRequestInserted = await _context.SaveChangesAsync();
                if (ispaymentRequestInserted != 1)
                    return response;
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = await streamReader.ReadToEndAsync();

                if (result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashCreatePaymentWithAgreementResponse>(result);
                    resultData.client_UserID = client_UserID;
                    var mapResponse = _mapper.Map<PaymentResponseDto>(resultData);
                    var storePaymentResponseDataIntoDb = await _context.BkashCreatePaymentWithAgreementResponses.AddAsync(resultData);
                    var isResponseStored = await _context.SaveChangesAsync();
                    if (isResponseStored > 0)
                        response.Data = mapResponse;
                    response.Message = "success";
                    response.TotalRecords = isResponseStored;
                    response.Status = true;
                    return response;
                    //resultData.merchantInvoiceNumber = string.IsNullOrEmpty(resultData.merchantInvoiceNumber) ? invoiceNumber : resultData.merchantInvoiceNumber;
                    // _context.BkashCreateAgreementResponses.Add(resultData);
                    //   _ = _context.SaveChangesAsync();
                    // return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }


                else
                {
                    response.Message = "failed";
                    response.Status = false;
                    response.Data = null;
                    return response;
                }


            }
            catch (Exception ex)
            {
                var aaa = ex.ToString();
                throw;
            }

        }
        public async Task<ServiceResponse<BkashExecuteAgreementRequest>> StoreExecuteAgreementRequestDataIntoDb(string paymentID)
        {
            var result = new ServiceResponse<BkashExecuteAgreementRequest> { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };
            var request = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = "" };
            try
            {
                var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync
                    (new BkashExecuteAgreementRequest { paymentID = request.paymentID, UserId = "" });
                var isReqDataStored = await _context.SaveChangesAsync();
                if (isReqDataStored > 0)
                result.Message = "success";
                result.Status = true;
                result.StatusCode = ResStatusCode.Success;
                //result.Data = storeRequestDataIntoDb.Entity;


            }
            catch (Exception ex)
            {
                var error = ex.Message;
                result.Status = false;
                result.StatusCode = ResStatusCode.InternalServerError;
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResponse<BkashExecuteAgreementResponse>> ExecuteAgreement3(string paymentID)
        {
            var result = new ServiceResponse<BkashExecuteAgreementResponse> { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };
            var storeRequestData = await StoreExecuteAgreementRequestDataIntoDb(paymentID);
            if (storeRequestData.Status == false)
            {
                result.Message = storeRequestData.Message;
                result.StatusCode = storeRequestData.StatusCode;
                //return result;
            }
           
            var request = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = "" };


            var requestData = JsonConvert.SerializeObject(request);
            try
            {
                
                var url = AppConstants.ExexAgreementUrl;
                var json = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = "" };
                string jsonString = JsonConvert.SerializeObject(json);
                var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", bKashToken.id_token);
                client.DefaultRequestHeaders.Add("X-APP-Key", AppConstants.tokenize_app_key);
                HttpResponseMessage response = await client.PostAsync(url, payload);
               // response.EnsureSuccessStatusCode();
                string responseJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    if (responseJson.Contains("paymentID"))
                    {
                        result.Message = "Success";
                        result.Status = true;
                        result.StatusCode = ResStatusCode.Success;

                        var successResponse = JsonConvert.DeserializeObject<BkashExecuteAgreementResponse>(responseJson);
                        successResponse.UserId = "";
                      //  successResponse.client_UserID = storeRequestData.Data.client_UserID;
                        //successResponse.bkashExecuteAgreementRequestId = storeRequestDataIntoDb.Entity.Id;
                        var storeResponseDataIntoDb = await _context.BkashExecuteAgreementResponses.AddAsync(successResponse);
                        var isResponseDataStored = await _context.SaveChangesAsync();
                        result.Data = successResponse;
                        return result;
                    }
                    else
                    {
                        var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(responseJson);
                        var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                        _ = _context.BKashAgreementExceptions.AddAsync(exception);
                        _ = _context.SaveChangesAsync();

                        result.Message = "Failed";
                        result.Status = false;
                        result.StatusCode = ResStatusCode.BadRequest;
                        result.Data = null;
                    }
                }
                
               
            }
            catch (Exception ex)
            {
                var exceptipn = new BKashAgreementException();
                exceptipn.statusMessage = ex.Message;
                // var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                //  var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                _ = _context.BKashAgreementExceptions.AddAsync(exceptipn);
                _ = _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = null;
            }

            return result;

        }
        public async Task HttpCLientPostCallAsync(string paymentID)
        {
            var result = new ServiceResponse<BkashExecuteAgreementResponse> { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };
            var url = AppConstants.ExexAgreementUrl;
            var json = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = "" };
            string jsonString = JsonConvert.SerializeObject(json);
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", bKashToken.id_token);
            client.DefaultRequestHeaders.Add("X-APP-Key", AppConstants.tokenize_app_key);
            HttpResponseMessage response = await client.PostAsync(url, payload);
            response.EnsureSuccessStatusCode();
            string responseJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
            }
            if (!response.IsSuccessStatusCode)
            {
                
            }


        }


        public async Task<ServiceResponse<BkashExecuteAgreementResponse>> ExecuteAgreement2(string paymentID)
        {
            //  CommonResponse result = new CommonResponse { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };
            var result = new ServiceResponse<BkashExecuteAgreementResponse> { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };

            var url = AppConstants.ExexAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;



            // var resId = _context.BkashCreateAgreementResponses.AsNoTracking().FirstOrDefault(s => s.paymentID == paymentID).UserId;
            var request = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = "" };

            var requestData = JsonConvert.SerializeObject(request);
            try
            {
                var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID, UserId = "" });
                // var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID ,bkashCreateAgreementResponseId =resId.Id });
                var isReqDataStored = await _context.SaveChangesAsync();
                if (isReqDataStored != 1)

                    return result;
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());

                var resultResponse = await streamReader.ReadToEndAsync();

                if (resultResponse.Contains("paymentID"))
                {
                    result.Message = "Success";
                    result.Status = true;
                    result.StatusCode = ResStatusCode.Success;

                    var successResponse = JsonConvert.DeserializeObject<BkashExecuteAgreementResponse>(resultResponse);
                    successResponse.UserId = "";
                    //successResponse.bkashExecuteAgreementRequestId = storeRequestDataIntoDb.Entity.Id;
                    var storeResponseDataIntoDb = await _context.BkashExecuteAgreementResponses.AddAsync(successResponse);
                    var isResponseDataStored = await _context.SaveChangesAsync();
                    result.Data = successResponse;
                    return result;
                }
                else
                {
                    var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                    var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                    _ = _context.BKashAgreementExceptions.AddAsync(exception);
                    _ = _context.SaveChangesAsync();

                    result.Message = "Failed";
                    result.Status = false;
                    result.StatusCode = ResStatusCode.BadRequest;
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                var exceptipn = new BKashAgreementException();
                exceptipn.statusMessage = ex.Message;
                // var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                //  var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                _ = _context.BKashAgreementExceptions.AddAsync(exceptipn);
                _ = _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = null;
            }

            return result;

        }

        public async Task<CommonResponse> ExecuteAgreement(string paymentID)
        {
            CommonResponse result = new CommonResponse { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };

            var url = AppConstants.ExexAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;



            var resId = await _context.BkashCreateAgreementResponses.FirstOrDefaultAsync(s => s.paymentID == paymentID);
            var request = new bKashExecAgreementRequestVM { paymentID = paymentID, userId = resId.UserId };

            var requestData = JsonConvert.SerializeObject(request);

            var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID, UserId = request.userId });
            // var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID ,bkashCreateAgreementResponseId =resId.Id });
            var isReqDataStored = await _context.SaveChangesAsync();
            if (isReqDataStored != 1)
                return result;
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(requestData);
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                result.Message = "Success";
                result.Status = true;
                result.StatusCode = ResStatusCode.Success;

                var successResponse = JsonConvert.DeserializeObject<BkashExecuteAgreementResponse>(resultResponse);
                successResponse.UserId = resId.UserId;
                // successResponse.bkashExecuteAgreementRequestId = storeRequestDataIntoDb.Entity.Id;
                var storeResponseDataIntoDb = await _context.BkashExecuteAgreementResponses.AddAsync(successResponse);
                var isResponseDataStored = await _context.SaveChangesAsync();
                result.Data = successResponse;
                return result;
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                _ = _context.BKashAgreementExceptions.AddAsync(exception);
                _ = _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = exception;
            }
            return result;
            //throw new NotImplementedException();
        }

        public async Task<CommonResponse> ExecutePayment(string paymentID)
        {
            CommonResponse result = new CommonResponse { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };
            var bKashToken = GenerateTokenForPaymentWithAgreement();
            var url = AppConstants.ExexAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var request = new BkashExecutePaymentVM { paymentID = paymentID };

            var requestData = JsonConvert.SerializeObject(request);
            var storeRequestDataIntoDb = await _context.BkashExecutePaymentWithAgreementRequests.AddAsync(new BkashExecutePaymentWithAgreementRequest { paymentID = request.paymentID });
            var isReqDataStored = await _context.SaveChangesAsync();
            if (isReqDataStored != 1)
                return result;
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(requestData);
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                result.Message = "Success";
                result.Status = true;
                result.StatusCode = ResStatusCode.Success;

                var successResponse = JsonConvert.DeserializeObject<BkashExecutePaymentWithAgreementResponse>(resultResponse);
                var storeResponseDataIntoDb = await _context.BkashExecutePaymentWithAgreementResponses.AddAsync(successResponse);
                var isResponseDataStored = await _context.SaveChangesAsync();
                result.Data = successResponse;
                return result;
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                _ = _context.BKashAgreementExceptions.AddAsync(exception);
                _ = _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = exception;
            }
            return result;


        }

        public BkashTokenResponse GenerateToken()
        {
            try
            {

                var httpRequest = (HttpWebRequest)WebRequest.Create(AppConstants.TokenUrl);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers["password"] = AppConstants.tokenize_pass;
                httpRequest.Headers["username"] = AppConstants.tokenize_username;
                var tonekRequest = new TokenRequest(AppConstants.tokenize_app_secret, AppConstants.tokenize_app_key);
                var requestData = Newtonsoft.Json.JsonConvert.SerializeObject(tonekRequest);
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();


                if (result.Contains("token_type"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashTokenResponse>(result);

                    return resultData;
                }
                else if (result.Contains("fail"))
                {
                    return new BkashTokenResponse();
                }
                else
                {
                    return new BkashTokenResponse();
                }
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}



            }
            catch (Exception exc)
            {


                return new BkashTokenResponse();
            }
        }

        public BkashTokenResponse GenerateTokenForPaymentWithAgreement()
        {
            try
            {

                var httpRequest = (HttpWebRequest)WebRequest.Create(AppConstants.GrantTokenForPaymentWithAgreement);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers["password"] = AppConstants.tokenize_pass;
                httpRequest.Headers["username"] = AppConstants.tokenize_username;
                var tonekRequest = new TokenRequest(AppConstants.tokenize_app_secret, AppConstants.tokenize_app_key);
                var requestData = Newtonsoft.Json.JsonConvert.SerializeObject(tonekRequest);
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();


                if (result.Contains("token_type"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashTokenResponse>(result);

                    return resultData;
                }
                else if (result.Contains("fail"))
                {
                    return new BkashTokenResponse();
                }
                else
                {
                    return new BkashTokenResponse();
                }
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}



            }
            catch (Exception exc)
            {


                return new BkashTokenResponse();
            }

            //throw new NotImplementedException();
        }

        public async Task<BkashCreateAgreementRequest> GetClientUrl(string paymentId)
        {
            var paymentIdBkash = await _context.BkashCreateAgreementResponses.FirstOrDefaultAsync(s => s.paymentID == paymentId);

            var data = await _context.BkashCreateAgreementRequests.FirstOrDefaultAsync(s => s.merchantInvoiceNumber == paymentIdBkash.merchantInvoiceNumber);
            return data;
        }

        public async Task<BkashExecuteAgreementResponse> GetAgreementIdByPaymentId(string paymentID)
        {
            var data = await _context.BkashExecuteAgreementResponses.FirstOrDefaultAsync(s => s.paymentID == paymentID);
            return data;
        }

        public async Task<BkashUserClientUrlAndInvoiceDto> GetClientURLByUserId(string paymentID)
        {
            var d = await _context.BkashCreateAgreementResponses.Where(s => s.paymentID == paymentID).Select(p => new BkashUserClientUrlAndInvoiceDto()
            {
                clientSuccess_redirectURL =p.clientSuccess_redirectURL,
                client_invoiceNUmber=p.client_invoiceNUmber
               
            }).FirstOrDefaultAsync();
                //var data = await _context.BkashCreateAgreementResponses.AsNoTracking().FirstOrDefaultAsync(s => s.paymentID == paymentID);
                //var mapData = new BkashUserClientUrlAndInvoiceDto()
                //{
                //    client_invoiceNUmber = data.client_invoiceNUmber,
                //    clientSuccess_redirectURL = data.clientSuccess_redirectURL
                //};
                return d;
           
            

        }

        public async Task<bool> CheckUserExist(string userId)
        {
            var data = await _context.BkashCreateAgreementRequests.FirstOrDefaultAsync(s => s.UserId == userId);
            if (data != null)
                return true;
            return false;

        }

        public async Task<BkashTokenResponse> GenerateTokenForAgreementCancel()
        {
            try
            {

                var httpRequest = (HttpWebRequest)WebRequest.Create(AppConstants.CancelAgreementTokenURL);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers["password"] = AppConstants.tokenize_pass;
                httpRequest.Headers["username"] = AppConstants.tokenize_username;
                var tonekRequest = new TokenRequest(AppConstants.tokenize_app_secret, AppConstants.tokenize_app_key);
                var requestData = Newtonsoft.Json.JsonConvert.SerializeObject(tonekRequest);
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();


                if (result.Contains("token_type"))
                {
                    var resultData = JsonConvert.DeserializeObject<BkashTokenResponse>(result);

                    return resultData;
                }
                else if (result.Contains("fail"))
                {
                    return new BkashTokenResponse();
                }
                else
                {
                    var resultData = JsonConvert.DeserializeObject<object>(result);


                    return new BkashTokenResponse
                    {
                        id_token = resultData.ToString(),
                    };
                }

            }
            catch (Exception exc)
            {

                var error = exc.Message;
                return new BkashTokenResponse
                {
                    id_token = error
                };
            }
        }

        public async Task<ServiceResponse<BkashCancelAgreementDto>> BkashCancelAgreement(string agreementID)
        {
            var bkash_Token = await GenerateTokenForAgreementCancel();
            //  var response = new ServiceResponse<BkashCancelAgreementResponse>();
            var result = new ServiceResponse<BkashCancelAgreementDto> { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };

            var url = AppConstants.BkashCancelAgreementURL;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bkash_Token.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var request = new BkashCancelAgreementVM { agreementID = agreementID };

            var requestData = JsonConvert.SerializeObject(request);
            try
            {
                //   var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID, UserId = "" });
                // var storeRequestDataIntoDb = await _context.BkashExecuteAgreementRequests.AddAsync(new BkashExecuteAgreementRequest { paymentID = request.paymentID ,bkashCreateAgreementResponseId =resId.Id });
                //  var isReqDataStored = await _context.SaveChangesAsync();
                //if (isReqDataStored != 1)

                //  return result;
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());

                var resultResponse = await streamReader.ReadToEndAsync();

                if (resultResponse.Contains("paymentID"))
                {
                    var successResponse = JsonConvert.DeserializeObject<BkashCancelAgreementResponse>(resultResponse);
                    var mapResultResponseData = _mapper.Map<BkashCancelAgreementDto>(successResponse);
                    var updateAgreementStatus = await _context.BkashExecuteAgreementResponses.FirstOrDefaultAsync(s => s.agreementID == agreementID);
                    updateAgreementStatus.agreementStatus = successResponse.agreementStatus;
                    updateAgreementStatus.ModifiedDate = DateTime.Now;
                    result.Message = "Success";
                    result.Status = true;
                    result.StatusCode = ResStatusCode.Success;


                    // successResponse.UserId = resId;
                    //successResponse.bkashExecuteAgreementRequestId = storeRequestDataIntoDb.Entity.Id;
                    var storeResponseDataIntoDb = await _context.BkashCancelAgreementResponse.AddAsync(successResponse);
                    var isResponseDataStored = await _context.SaveChangesAsync();
                    result.Data = mapResultResponseData;
                    return result;
                }
                else
                {
                    ResStatusCode code;
                    var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                    var errorMsg = resultData.statusMessage;
                    var errorCode = resultData.statusCode;
                    var bkashError = Enum.Parse<ResStatusCode>(errorCode);
                    var exception = new BKashAgreementException { statusCode = resultData.statusCode, agreementID = agreementID, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null };
                    var storeExceptionDataInDb = await _context.BKashAgreementExceptions.AddAsync(exception);
                    var isStored = await _context.SaveChangesAsync();

                    result.Message = errorMsg;
                    result.Status = false;
                    result.StatusCode = bkashError;
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                var exceptipn = new BKashAgreementException();
                exceptipn.statusMessage = ex.Message;
                // var resultData = JsonConvert.DeserializeObject<BKashAgreementExceptionVM>(resultResponse);
                //  var exception = new BKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };
                var storeExceptionData = await _context.BKashAgreementExceptions.AddAsync(exceptipn);
                var isStored = await _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = null;
            }
            return result;

        }

        public async Task<ServiceResponse<List<UserAgreementsDto>>> GetAgreementIDByUserID(string userId)
        {
            var response = new ServiceResponse<List<UserAgreementsDto>>();
            try
            {
                var userAgreementData = await _context.BkashExecuteAgreementResponses.AsNoTracking().Where(s => s.payerReference == userId
                && s.agreementStatus == "Completed").ToListAsync();
                var mapResponse = _mapper.Map<List<UserAgreementsDto>>(userAgreementData);

                if (userAgreementData.Count > 0)
                {
                    response.Message = "user agreementID";
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    response.TotalRecords = userAgreementData.Count();
                    response.Data = mapResponse;
                }
                else
                {
                    response.Message = "no data found";
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    response.TotalRecords = userAgreementData.Count();
                    response.Data = null;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.TotalRecords = 0;
                response.Data = null;

            }

            return response;

        }
    }
}
