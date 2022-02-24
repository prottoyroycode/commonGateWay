using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse;
using Bkash_Service_API.Models.Response;
using Bkash_Service_API.Services.BkashRecurringPaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Controllers
{
    public class BkashRecurringController:BaseApiController
    {
        private readonly IBkashRecurringService _bkashRecurringService;
        private readonly ILogger<BkashRecurringController> _logger;
        public BkashRecurringController(IBkashRecurringService bkashRecurringService , ILogger<BkashRecurringController> logger)
        {
            _bkashRecurringService = bkashRecurringService;
            _logger = logger;
        }
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    // return Redirect("partnersintregation.techapi24.com/api/v1/BkashRecurring/redirectUrlget");
        //    return Ok("success");
        //}
        [HttpPost]
        public async Task<IActionResult> Get()
        {
            // return Redirect("partnersintregation.techapi24.com/api/v1/BkashRecurring/redirectUrlget");
            return Ok("success");
        }
        [HttpGet]
        public async Task<IActionResult> RedirectUrl(string data)
        {
           // return Redirect("partnersintregation.techapi24.com/api/v1/BkashRecurring/redirectUrlget");
            return Ok(data);
        }
        [HttpPost("redirectUrl")]
        public async Task<IActionResult> RedirectUrl([FromBody] CreateSubscriptionResponse model)
        {

            return Ok(model);
        }

      //  [Authorize(AuthenticationSchemes ="Bearer",Roles =Role.FunBox_Recurring_Bkash)]
        [HttpPost("initiateSubscription")]
        public async Task<IActionResult> CreateSubscriptionInitiate([FromBody] CreateSubscriptionInitiateBkashDto model)
        {
            var res = new ServiceResponse<CreateSubscriptionInitiateBkashDto>();
            //var checkUserId = await _bkashRecurringService.CheckIfUserExists(model.userId);
            //if (checkUserId)
            //{

            //    res.Data = null;
            //    res.Status = false;
            //     res.Message = "user already exists";
            //    res.TotalRecords = 1;
            //    res.StatusCode = ResStatusCode.BadRequest;
            //    return Ok(res);
            //}
            try
            {

                _logger.LogInformation("this is from CreateSubscriptionInitiate() ");

                var data = await _bkashRecurringService.BkashRecurringCreateSubAsync(model);
                return Ok(data);


            }
           catch(Exception ex)
            {
                var exMsg = ex.Message;
                return BadRequest("sorry we could not proceed  your requset");
            }
            //model.subscriptionRequestId = "new-2";
            //model.serviceId = 1;
            //model.redirectURL = model.redirectURL;
            //model.paymentType = "FIXED";
            //model.subscriptionType = "BASIC";
           // model.amount = 10;
            //model.firstPaymentAmount = 1;
            //model.currency = "BDT";
            //model.firstPaymentIncludedInCycle = false;
            //model.maxCapAmount = 1;
            //model.frequecy = "DAILY";
            //model.startDate = "2022-02-08";
            //model.expiryDate= "2022-02-09";
            //model.merchantShortCode = "50011";
            //model.payerType = "CUSTOMER";
            //model.payer = "01870725511";
            //model.subscriptionReference = "ref-1";
            //model.maxCapRequired = false;
           
        }
       // [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.FunBox_Recurring_Bkash)]
        [HttpPost("querySubscription")]
        public async Task<IActionResult> QuerySubscriptionAndPaymentAfterCallBack(QuerySubscriptionAndPaymentRequestDto model)
        {
            var data = await _bkashRecurringService.BkashQuerySubscriptionAndPaymentAfterCallBackAsync(model.reference);
            return Ok(data);
        }
      // [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.FunBox_Recurring_Bkash)]
        [HttpPost("cancelSubscription")]
        public async Task<IActionResult> CancelSubscription([FromBody] CancelSubscriptionRequestDto model)
        {
            
            var data = await _bkashRecurringService.CancelSubscriptionAsync(model.subscriptionId);
            return Ok(data);
        }

     //   [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.FunBox_Recurring_Bkash)]
        [HttpPost("get-paymentlist")]
        public async Task<IActionResult> GetPaymentList([FromBody] GetPaymentListRequestDto model)
        {

            var data = await _bkashRecurringService.GetPaymentListBySubscriptionIdAsync(model.subscriptionId);
            return Ok(data);
        }
      //  [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.FunBox_Recurring_Bkash)]
        [HttpPost("getpaymentinfobyPaymentId")]
        public async Task<IActionResult> GetPaymentInfoByPaymentId([FromBody] GetpaymentinfobyPaymentIdRequestDto model)
        {

            var data = await _bkashRecurringService.GetPaymentInfoByPaymentId(model.paymentId);
            return Ok(data);
        }

        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody] RefundRequestDto model )
        {

            var data = await _bkashRecurringService.RefundPaymentAsync(model.paymentId,model.amount);
            return Ok(data);
        }
        [HttpPost("queryBySubscriptionId")]
        public async Task<IActionResult> QueryBySubscriptionId([FromBody] QueryBySubscriptionIdRequestDto model)
        {

            var data = await _bkashRecurringService.QueryBySubscriptionId(model.subscriptionId);
            return Ok(data);
        }

        [HttpPost("searchpaymentlist")]
        public async Task<IActionResult> SubscriptionListWitPaging([FromBody] SubscriptionListPagingRequestDto model)
        {

            var data = await _bkashRecurringService.SubscriptionList(model.pageNumber,model.size);
            return Ok(data);
        }
        [HttpPost("getPaymentSchedule")]
        public async Task<IActionResult> GetPaymentSchedule([FromBody] GetScheduleRequestDto model)
        {

            var data = await _bkashRecurringService.GetPaymentScheduleUrl(model.freequency, model.startDate,model.endDate);
            return Ok(data);
        }

        [HttpGet("checkSubscriptionStatus")]
        public async Task<IActionResult> CheckUserStatus(string userId)
        {

            var data = await _bkashRecurringService.CheckUsersSubscriptionStatus(userId);
            return Ok(data);
        }



    }
}
