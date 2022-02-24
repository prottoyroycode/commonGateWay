using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class UserSubscriptionStatusDto
    {
        public string userId { get; set; }
        public string subscriptionRequestId { get; set; }
        public int subscriptionId { get; set; }
        public string dueDate { get; set; }
        public string nextPaymentDate { get; set; }
        public string frequency { get; set; }
        public string subscriptionStatus { get; set; }
    }
}
