using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class GetPaymentBySubscriptionIdResponseDto
    {
        public int id { get; set; }
        public int subscriptionId { get; set; }
        public string dueDate { get; set; }
        public string status { get; set; }
        public string trxId { get; set; }
        public DateTime trxTime { get; set; }
        public decimal amount { get; set; }
        public string reverseTrxAmount { get; set; }
        public string reverseTrxId { get; set; }
        public string reverseTrxTime { get; set; }
    }
}
