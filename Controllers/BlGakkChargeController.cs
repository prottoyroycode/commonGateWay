using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse;
using Bkash_Service_API.Models.Response;
using Bkash_Service_API.Services.BLChargeGakkService;
using Bkash_Service_API.Services.BLTouchAndPlayService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bkash_Service_API.Controllers
{
    public class BlGakkChargeController : BaseApiController
    {

        private readonly IBLChargeGakkServiceProvider _gakkBlService;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public BlGakkChargeController(IBLChargeGakkServiceProvider gakkBlService , IHttpContextAccessor httpContextAccessor)
        {
            _gakkBlService = gakkBlService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglaLinkGakkService)]
        [HttpGet("checksubscription/{msisdn}/{gakkServiceId}")]
        public async Task<IActionResult> Checksubscription(string msisdn , string gakkServiceId)
        {

            string clientId =  _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = await _gakkBlService.CheckSubscription(msisdn, gakkServiceId , clientId);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglaLinkGakkService)]
        [HttpGet("charge/{msisdn}/{gakkServiceId}")]
        public async Task<IActionResult> Charge(string msisdn, string gakkServiceId)
        {

            string clientId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = await _gakkBlService.Charge(msisdn, gakkServiceId , clientId);
            return Ok(data);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.BanglaLinkGakkService)]
        [HttpGet("Unsubscribe/{msisdn}/{gakkServiceId}")]
        public async Task<IActionResult> Unsubscribe(string msisdn, string gakkServiceId)
        {

            string clientId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = await _gakkBlService.CancelSubscription(msisdn, gakkServiceId, clientId);
            return Ok(data);
        }


    }
}
