using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse;
using Bkash_Service_API.Models.Response;
using Bkash_Service_API.Services.BLTouchAndPlayService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Controllers
{
    public class BLChargeController : BaseApiController
    {
        private readonly IBLTouchAndPlayService _bLTouchAndPlayService;
        public BLChargeController(IBLTouchAndPlayService bLTouchAndPlayService)
        {
            _bLTouchAndPlayService = bLTouchAndPlayService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglalinkService_TouchANd_Play)]
        [HttpPost("generate-otp")]
        public async Task<IActionResult> GenerateOtp([FromBody] BLTouchAndPlayOtpReqDto bLTouchAndPlayOtpReqDto)
        {


            //bLTouchAndPlayOtpReqDto.Msisdn = "8801981165548";
            //bLTouchAndPlayOtpReqDto.Msisdn = "8801951101955";
            //bLTouchAndPlayOtpReqDto.SubscriptionGroupID = "DCB_Touch_and_Play_Daily_Auto";
            var data = await _bLTouchAndPlayService.OtpGenerateAsync(bLTouchAndPlayOtpReqDto);
            return Ok(data);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglalinkService_TouchANd_Play)]
        [HttpPost("otp-validation")]
        public async Task<IActionResult> OtpValidation([FromBody] BLTouchAndPlayOtpValidationRequestDto model)
        {
            //model.msisdn = "8801951101955";
            //model.SubscriptionGroupID = "DCB_Touch_and_Play_Daily_Auto";
            //model.serviceId = "DCB_Touch_and_Play_Daily_Auto";
            //model.subRoot = "DCB_Touch_and_Play";
            var data = await _bLTouchAndPlayService.OtpValidationAsync(model);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglalinkService_TouchANd_Play)]
        [HttpPost("check-status")]
        public async Task<IActionResult> CheckStatus([FromBody] BLTouchAndPlayOtpReqDto bLTouchAndPlayOtpReqDto)
        {


            ////bLTouchAndPlayOtpReqDto.Msisdn = "8801981165548";
            //bLTouchAndPlayOtpReqDto.Msisdn = "8801951101955";
            //bLTouchAndPlayOtpReqDto.SubscriptionGroupID = "DCB_Touch_and_Play_Daily_Auto";
            var data = await _bLTouchAndPlayService.CheckStatus(bLTouchAndPlayOtpReqDto);
            return Ok(data);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglalinkService_TouchANd_Play)]
        [HttpPost("cancel-subscribtipn")]
        public async Task<IActionResult> CancelSubscription([FromBody] BLCancelSubscriptionDto model)
        {
            //model.channel = "web";
            //model.msisdn = "8801981165548";
            //model.subRoot = "DCB_Touch_and_Play";
            var data = await _bLTouchAndPlayService.CancelSubscribtion(model);
            return Ok(data);
        }

    }
}
