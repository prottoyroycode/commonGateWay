using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class RefundErrorResponseDto
    {
        public string status { get; set; }
        public string reason { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
