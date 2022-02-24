using Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse;
using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BLTouchAndPlayService
{
    public interface IBLTouchAndPlayService
    {
        Task<ServiceResponse<BLTouchAndPlayOtpResponseDto>> OtpGenerateAsync(BLTouchAndPlayOtpReqDto bLTouchAndPlayOtpReqDto);
        Task<ServiceResponse<BLTouchAndPlayOtpResponseDto>> OtpValidationAsync(BLTouchAndPlayOtpValidationRequestDto bLTouchAndPlayOtpReqDto);
        Task<ServiceResponse<BLTouchAndPlayStatusResponseDto>> CheckStatus(BLTouchAndPlayOtpReqDto model);

        Task<ServiceResponse<BLCancelSubResponseDto>> CancelSubscribtion(BLCancelSubscriptionDto model);


    }
}
