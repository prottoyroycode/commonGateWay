using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Response
{
    public class BLGakkServiceCheckSubscriptionResponse
    {
        public bool status { get; set; }
        public string subscriptionStatus { get; set; }
        public string msisdn { get; set; }
        public string service { get; set; }
        public string nextRenewDate { get; set; }
    }
}
