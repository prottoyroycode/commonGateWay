using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class CreateSubscriptionResponseDto
    {
        [JsonIgnore]
        public string subscriptionRequestId { get; set; }
        public string redirectURL { get; set; }
        public string expirationTime { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
