using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class BkashQuerySubscriptionAndPaymentDto
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public string subscriptionRequestId { get; set; }
        public int requesterId { get; set; }
        public int serviceId { get; set; }
        public string paymentType { get; set; }
        public string subscriptionType { get; set; }
        public string amountQueryUrl { get; set; }
        public decimal amount { get; set; }
        public string firstPaymentAmount { get; set; }
        public bool maxCapRequired { get; set; }
        public string maxCapAmount { get; set; }
        public string frequency { get; set; }
        public string startDate { get; set; }
        public string expiryDate { get; set; }
        public int merchantId { get; set; }
        public string payerType { get; set; }
        public string payer { get; set; }
        public string currency { get; set; }
        public string nextPaymentDate { get; set; }
        public string status { get; set; }
        public string subscriptionReference { get; set; }
        public string extraParams { get; set; }
        public string cancelledBy { get; set; }
        public string cancelledTime { get; set; }
        public bool enabled { get; set; }
        public bool active { get; set; }
        public bool expired { get; set; }
        public string rrule { get; set; }
    }
}
