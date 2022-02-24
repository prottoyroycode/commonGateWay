using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class SubscriptionListPagingRequestDto
    {
        public int pageNumber { get; set; }
        public int size { get; set; }
    }
}
