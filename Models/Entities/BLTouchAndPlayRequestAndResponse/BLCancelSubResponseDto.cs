using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse
{
    public class BLCancelSubResponseDto
    {
        public string Msisdn { get; set; }
        public string Message { get; set; }
        public string SubscriptionRootId { get; set; }
    }
}
