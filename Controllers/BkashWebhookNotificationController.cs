using AutoMapper;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse;
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
    public class BkashWebhookNotificationController:BaseApiController
    {
        public readonly IMapper _mapper;
        public readonly IBkashRecurringService _bkashRecurringService;
        private readonly ILogger<BkashWebhookNotificationController> _logger;
        public BkashWebhookNotificationController(IMapper mapper , IBkashRecurringService bkashRecurringService , ILogger<BkashWebhookNotificationController> logger)
        {
            _mapper = mapper;
            _bkashRecurringService = bkashRecurringService;
            _logger = logger;

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BkashWebhookNotificaion([FromBody] RecurringWebhookNotificationDto recurringWebhookNotification)
        {
            try
            {
                
                _logger.LogInformation("notification starts in BkashWebhookNotificaion()");
                _logger.LogInformation(recurringWebhookNotification.ToString());
                //recurringWebhookNotification.message = null;
                var result = await _bkashRecurringService.BkashWebhookNotificationAsync(recurringWebhookNotification);
                return Ok(result);
            }
            catch(Exception)
            {
                return BadRequest("we are sorry");
            }
            
        }
    }
}
