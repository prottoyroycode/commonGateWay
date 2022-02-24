using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BLChargeGakkService
{
   public interface IBLChargeGakkServiceProvider
    {
        Task<CommonResponse> Charge(string msisdn, string gakkServiceId , string clientId);
        Task<CommonResponse> CancelSubscription(string msisdn, string gakkServiceId , string clientId);
        Task<CommonResponse> CheckSubscription(string msisdn, string gakkServiceId, string clientId);

    }
}
