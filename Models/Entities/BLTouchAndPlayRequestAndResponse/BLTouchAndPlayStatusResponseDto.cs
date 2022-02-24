using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse
{
    public class BLTouchAndPlayStatusResponseDto
    {
        public string msisdn { get; set; }
       // public string company { get; set; }
        public string serviceId { get; set; }
        public string subsubscriptionGroupId { get; set; }
        public string subscriptionRoot { get; set; }
        public string subscriptionStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
