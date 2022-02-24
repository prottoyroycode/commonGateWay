using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class CancelSubscriptionResponseDto
    {
        public string subscriptionId { get; set; }
        public string subscriptionStatus { get; set; }
        public string message { get; set; }
        public string instant { get; set; }
    }
}
