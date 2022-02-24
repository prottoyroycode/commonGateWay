using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class SubscriptionListResponseDto
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public List<Content> content { get; set; }
        public int currentPage { get; set; }
    }
    public class Content
    {
        public int id { get; set; }
        public string subscriptionRequestId { get; set; }
        public int merchantId { get; set; }
        public string merchantShortCode { get; set; }
        public string payer { get; set; }
        public decimal amount { get; set; }
        public string startDate { get; set; }
        public string expiryDate { get; set; }
        public string frequency { get; set; }
        public string status { get; set; }
        public string cancelledBy { get; set; }
        public string cancelledTime { get; set; }
        
    }
}
