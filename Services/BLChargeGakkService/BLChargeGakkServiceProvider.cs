using Bkash_Service_API.Data;
using Bkash_Service_API.Models.Entities.BLChargeWithGakkService;
using Bkash_Service_API.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BLChargeGakkService
{
    public class BLChargeGakkServiceProvider : IBLChargeGakkServiceProvider
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly DataContext _context;
        public BLChargeGakkServiceProvider(IHttpClientFactory clientFactory , DataContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }
        public async Task<CommonResponse> CancelSubscription(string msisdn, string gakkServiceId , string clientId)
        {
            var result = new CommonResponse() { Status = false, TotalRecords = 0, Message = "Bad Request", StatusCode = ResStatusCode.BadRequest };

            BLChargeGakkServiceUnsubscribeRequest aRequest = new BLChargeGakkServiceUnsubscribeRequest();

            aRequest.Id = Guid.NewGuid();
            aRequest.Msisdn = msisdn;
            aRequest.UserId = clientId;
            aRequest.GakkServiceId = gakkServiceId;

            _ = await _context.BLChargeGakkServiceUnsubscribeRequests.AddAsync(aRequest);
            _ = await _context.SaveChangesAsync();


            var client = _clientFactory.CreateClient("dcb_banglaLink_gakkService");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = string.Format("http://27.131.15.18:901/api/BLCharging/CancelSubscription/{0}/{1}/{2}", msisdn, gakkServiceId, clientId);
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BLChargeGakkServiceUnsubscribeResponse>(aresult);
                data.Id = Guid.NewGuid();
                data.requestId = aRequest.Id.ToString();
                result = new CommonResponse() { Status = true, Data = data, TotalRecords = 0, Message = "Success", StatusCode = ResStatusCode.Success };
            }
            return result;
        }

        public async Task<CommonResponse> Charge(string msisdn, string gakkServiceId , string clientId)
        {
            var result = new CommonResponse() { Status = false, TotalRecords = 0, Message = "Bad Request", StatusCode = ResStatusCode.BadRequest };

            BLChargeGakkServiceChargeRequest aRequest = new BLChargeGakkServiceChargeRequest();
            aRequest.ClientId = clientId;
            aRequest.Id = Guid.NewGuid();
            aRequest.GakkServiceId = gakkServiceId;
            aRequest.client_UserID = clientId;
            aRequest.Msisdn = msisdn;
            _ = await _context.BLChargeGakkServiceChargeRequests.AddAsync(aRequest);
            _ = await _context.SaveChangesAsync();
            var client = _clientFactory.CreateClient("dcb_banglaLink_gakkService");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = string.Format("http://27.131.15.18:901/api/BLCharging/chargeuser/{0}/{1}/{2}", msisdn, gakkServiceId,clientId);
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BLChargeGakkServiceChargeResponse>(aresult);
                data.Id = Guid.NewGuid();
                data.requestId = aRequest.Id.ToString();
                _ = await _context.BLChargeGakkServiceChargeResponses.AddAsync(data);
                _ = await _context.SaveChangesAsync();
                result = new CommonResponse() { Status = true, Data = data, TotalRecords = 0, Message = "Success", StatusCode = ResStatusCode.Success };
            }
            return result;
        }

        public async Task<CommonResponse> CheckSubscription(string msisdn, string gakkServiceId , string clientId)
        {

            var result = new CommonResponse() { Status = false, TotalRecords = 0, Message = "Bad Request", StatusCode = ResStatusCode.BadRequest };
            var client = _clientFactory.CreateClient("dcb_banglaLink_gakkService");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = string.Format("http://27.131.15.18:901/api/BLCharging/CheckSubscription/{0}/{1}/{2}", msisdn, gakkServiceId , clientId);
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var  aresult = httpResponse.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BLGakkServiceCheckSubscriptionResponse>(aresult);
                result = new CommonResponse() { Status = true, Data = data, TotalRecords = 0, Message = "Success", StatusCode = ResStatusCode.Success };
            }

            return result;
        }
    }
}
