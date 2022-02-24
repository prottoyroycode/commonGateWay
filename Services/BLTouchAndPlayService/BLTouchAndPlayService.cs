using AutoMapper;
using Bkash_Service_API.Data;
using Bkash_Service_API.Models.Entities.BLTouchAndPlayModels;
using Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse;
using Bkash_Service_API.Models.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BLTouchAndPlayService
{

    public class BLTouchAndPlayService : IBLTouchAndPlayService
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BLTouchAndPlayService(DataContext context, IMapper mapper, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GetUserId() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private async Task<ServiceResponse<BLTouchAndPlayOtpRequest>> StoreOtpRequestData(BLTouchAndPlayOtpReqDto model)
        {
            var response = new ServiceResponse<BLTouchAndPlayOtpRequest>();
            var requestModel = new BLTouchAndPlayOtpRequest
            {
                Company = GetUserId(),
                Msisdn = model.Msisdn,
                SubscriptionGroupID = model.SubscriptionGroupID,

            };
            try
            {
                var storeDataIntoDb = await _context.BLTouchAndPlayOtpRequests.AddAsync(requestModel);
                var isStored = await _context.SaveChangesAsync();
                if (isStored > 0)
                    response.Status = true;
                response.Message = "success";
                response.StatusCode = ResStatusCode.Success;
                response.Data = requestModel;
                response.TotalRecords = 1;
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
                response.Message = exMsg;
                response.Status = false;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;

            }
            return response;
        }
        private async Task<ServiceResponse<BLTouchAndPlayOtpValidationRequest>> StoreOtpValidationRequestData(BLTouchAndPlayOtpValidationRequestDto model)
        {
            var response = new ServiceResponse<BLTouchAndPlayOtpValidationRequest>();
            var requestModel = new BLTouchAndPlayOtpValidationRequest
            {
                company = GetUserId(),
                msisdn = model.msisdn,
                subGroupId = model.SubscriptionGroupID,
                serviceId = model.serviceId,
                subRoot = model.subRoot

            };
            try
            {
                var storeDataIntoDb = await _context.BLTouchAndPlayOtpValidationRequests.AddAsync(requestModel);
                var isStored = await _context.SaveChangesAsync();
                if (isStored > 0)
                    response.Status = true;
                response.Message = "success";
                response.StatusCode = ResStatusCode.Success;
                response.Data = requestModel;
                response.TotalRecords = 1;
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
                response.Message = exMsg;
                response.Status = false;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;

            }
            return response;
        }

        public async Task<ServiceResponse<BLTouchAndPlayOtpResponseDto>> OtpGenerateAsync(BLTouchAndPlayOtpReqDto bLTouchAndPlayOtpReqDto)
        {
            var responseModel = new BLTouchAndPlayOtpResponseDto();

            var response = new ServiceResponse<BLTouchAndPlayOtpResponseDto>
            {
                Message = "failed",
                Status = false,
                StatusCode = ResStatusCode.BadRequest,
                Data = null
            };
            string company = GetUserId();
            //var msisdn = "8801981165548";
            //var dcb = "DCB_Touch_and_Play_Daily_Auto";
            var storerequestData = await StoreOtpRequestData(bLTouchAndPlayOtpReqDto);
            if (storerequestData.Status == false)
            {
                response.Status = false;
                response.Message = "failed";
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Data = null;
            }
            if (storerequestData.Status == true)
            {
                try
                {
                    var client = _clientFactory.CreateClient("dcb_touch_and_play");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = string.Format("http://27.131.15.18:900/api/BLService/otp/{0}/{1}/{2}",
                           company, bLTouchAndPlayOtpReqDto.Msisdn, bLTouchAndPlayOtpReqDto.SubscriptionGroupID);
                    var httpResponse = await client.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var desrializedObject = JsonConvert.DeserializeObject<BLTouchAndPlayOtpResponse>(result);
                        desrializedObject.BLTouchAndPlayOtpRequestId = storerequestData.Data.Id;
                        var storeResponseIntoDb = await _context.BLTouchAndPlayOtpResponses.AddAsync(desrializedObject);
                        var isInserted = await _context.SaveChangesAsync();
                        if (isInserted > 0)
                            responseModel.Message = desrializedObject.message;
                        responseModel.Msisdn = storerequestData.Data.Msisdn;
                        responseModel.SubscriptionGroupId = storerequestData.Data.SubscriptionGroupID;
                        response.Data = responseModel;
                        response.Message = "success";
                        response.TotalRecords = 1;
                        response.StatusCode = ResStatusCode.Success;
                        response.Status = true;

                    }
                    else
                    {
                        response.Data = null;
                        response.Message = "request failed ,please try again later";
                        response.TotalRecords = 0;
                        response.StatusCode = ResStatusCode.InternalServerError;
                        response.Status = false;
                    }


                    //else sent user not sent msg
                }
                catch (Exception ex)
                {
                    var exMsg = ex.Message;
                    response.Message = exMsg;
                }
            }



            return response;
        }

        public async Task<ServiceResponse<BLTouchAndPlayOtpResponseDto>> OtpValidationAsync(BLTouchAndPlayOtpValidationRequestDto request)
        {
            var responseModel = new BLTouchAndPlayOtpResponseDto();
            // var company = "gakk";
            var company = GetUserId();
            var response = new ServiceResponse<BLTouchAndPlayOtpResponseDto>
            {
                Message = "failed",
                Status = false,
                StatusCode = ResStatusCode.BadRequest,
                Data = null
            };
            var storerequestData = await StoreOtpValidationRequestData(request);
            if (storerequestData.Status == false)
            {
                response.Status = false;
                response.Message = "failed";
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Data = null;
            }
            if (storerequestData.Status == true)
            {
                try
                {
                    var client = _clientFactory.CreateClient("dcb_touch_and_play");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = string.Format("http://27.131.15.18:900/api/BLService/validate/{0}/{1}/{2}/{3}/{4}/{5}",
                           company, request.msisdn, request.SubscriptionGroupID, request.subRoot, request.otp,
                           request.serviceId);
                    var httpResponse = await client.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var desrializedObject = JsonConvert.DeserializeObject<BLTouchAndPlayOtpValidationResponse>(result);
                        desrializedObject.BLTouchAndPlayOtpvalidationRequestId = storerequestData.Data.Id;
                        var storeResponseIntoDb = await _context.BLTouchAndPlayOtpValidationResponses.AddAsync(desrializedObject);
                        var isInserted = await _context.SaveChangesAsync();
                        if (isInserted > 0)
                            responseModel.Message = desrializedObject.message;
                        responseModel.Msisdn = storerequestData.Data.msisdn;
                        responseModel.SubscriptionGroupId = storerequestData.Data.subGroupId;
                        response.Data = responseModel;
                        if(desrializedObject.status == "0")
                        {
                            response.Message = "success";
                            response.TotalRecords = 1;
                            response.StatusCode = ResStatusCode.Success;
                            response.Status = true;
                        }
                        if (desrializedObject.status == "1")
                        {
                            response.Message = "failed";
                            response.TotalRecords = 1;
                            response.StatusCode = ResStatusCode.Success;
                            response.Status = false;
                        }


                    }
                    else
                    {
                        response.Data = null;
                        response.Message = "request failed ,please try again later";
                        response.TotalRecords = 0;
                        response.StatusCode = ResStatusCode.InternalServerError;
                        response.Status = false;
                    }



                }
                catch (Exception ex)
                {
                    var exMsg = ex.Message;
                    response.Message = exMsg;
                }
            }

            return response;
        }

        public async Task<ServiceResponse<BLTouchAndPlayStatusResponseDto>> CheckStatus(BLTouchAndPlayOtpReqDto model)
        {



            var response = new ServiceResponse<BLTouchAndPlayStatusResponseDto>
            {
                Message = "failed",
                Status = false,
                StatusCode = ResStatusCode.BadRequest,
                Data = null
            };
            string company = GetUserId();
            //var msisdn = "8801981165548";
            //var dcb = "DCB_Touch_and_Play_Daily_Auto";


            try
            {
                var client = _clientFactory.CreateClient("dcb_touch_and_play");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("http://27.131.15.18:900/api/BLService/subscriptionstatus/{0}/{1}/{2}",
                       company, model.Msisdn, model.SubscriptionGroupID);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Content.ReadAsStringAsync().Result;
                    var desrializedObject = JsonConvert.DeserializeObject<BLTouchAndPlayStatusResponseDto>(result);
                    
                    response.Data = desrializedObject;
                    response.Message = "success";
                    response.TotalRecords = 1;
                    if (response.Data == null)
                    {
                        response.TotalRecords = 0;
                        response.Message = "no data found";
                    }
                    response.StatusCode = ResStatusCode.Success;
                    response.Status = true;

                }
                else
                {
                    response.Data = null;
                    response.Message = "request failed ,please try again later";
                    response.TotalRecords = 0;
                    response.StatusCode = ResStatusCode.InternalServerError;
                    response.Status = false;
                }


                //else sent user not sent msg
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
                response.Message = exMsg;
            }
            return response;
        }
        private async Task<ServiceResponse<BLTouchAndPlayCacelSubscriptionRequest>> StoreCancelSubRequestData(BLCancelSubscriptionDto model)
        {
            var response = new ServiceResponse<BLTouchAndPlayCacelSubscriptionRequest>();
            var requestModel = new BLTouchAndPlayCacelSubscriptionRequest
            {
                company = GetUserId(),
                msisdn = model.msisdn,
                subRoot = model.subRoot,
                channel=model.channel

            };
            try
            {
                var storeDataIntoDb = await _context.BLTouchAndPlayCacelSubscriptionRequests.AddAsync(requestModel);
                var isStored = await _context.SaveChangesAsync();
                if (isStored > 0)
                    response.Status = true;
                response.Message = "success";
                response.StatusCode = ResStatusCode.Success;
                response.Data = requestModel;
                response.TotalRecords = 1;
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
                response.Message = exMsg;
                response.Status = false;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;

            }
            return response;
        }
        public async Task<ServiceResponse<BLCancelSubResponseDto>> CancelSubscribtion(BLCancelSubscriptionDto model)
        {

            var responseModel = new BLCancelSubResponseDto();
            // var company = "gakk";
            var company = GetUserId();
            var response = new ServiceResponse<BLCancelSubResponseDto>
            {
                Message = "failed",
                Status = false,
                StatusCode = ResStatusCode.BadRequest,
                Data = null
            };
            var storerequestData = await StoreCancelSubRequestData(model);
            if (storerequestData.Status == false)
            {
                response.Status = false;
                response.Message = "failed";
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Data = null;
            }
            if (storerequestData.Status == true)
            {
                try
                {
                    var client = _clientFactory.CreateClient("dcb_touch_and_play");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = string.Format("http://27.131.15.18:900/api/BLService/UnSubscribe/{0}/{1}/{2}/{3}",
                           company, model.msisdn, model.subRoot,model.channel);
                    var httpResponse = await client.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var desrializedObject = JsonConvert.DeserializeObject<BLTouchAndPlayCacelSubscriptionResponse>(result);
                        desrializedObject.CancelSubRequestId = storerequestData.Data.Id;
                        var storeResponseIntoDb = await _context.BLTouchAndPlayCacelSubscriptionResponses.AddAsync(desrializedObject);
                        var isInserted = await _context.SaveChangesAsync();
                        if (isInserted > 0)
                            responseModel.Message = desrializedObject.message;
                        responseModel.Msisdn = storerequestData.Data.msisdn;
                        responseModel.SubscriptionRootId = storerequestData.Data.subRoot;
                        response.Data = responseModel;
                        response.Message = "success";
                        response.TotalRecords = 1;
                        response.StatusCode = ResStatusCode.Success;
                        response.Status = true;

                    }
                    else
                    {
                        response.Data = null;
                        response.Message = "request failed ,please try again later";
                        response.TotalRecords = 0;
                        response.StatusCode = ResStatusCode.InternalServerError;
                        response.Status = false;
                    }



                }
                catch (Exception ex)
                {
                    var exMsg = ex.Message;
                    response.Message = exMsg;
                }
            }

            return response;
        }
    }
}
