using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class RecurringWebhookNotificationDto
    {

        public object message { get; set; }
        public DateTime timeStamp { get; set; }

        public string subscriptionRequestId { get; set; }
        public int subscriptionId { get; set; }
        public string subscriptionStatus { get; set; }
        public string nextPaymentDate { get; set; }
        public decimal amount { get; set; }
        public string trxId { get; set; }
        public DateTime trxDate { get; set; }
        public int paymentId { get; set; }
        public string paymentStatus { get; set; }
        public string dueDate { get; set; }
        public bool firstPayment { get; set; }
        public string reverseTrxId { get; set; }
        public decimal refundedAmount { get; set; }
        public DateTime reversTrxDate { get; set; }
        public string cancelledBy { get; set; }

        // public string paymentId { get; set; }
        //public string paymentStatus { get; set; }
        //public bool firstPayment { get; set; }
        //public string dueDate { get; set; }
        //public string reverseTrxId { get; set; }
        //public decimal refundedAmount { get; set; }
        //public string reversTrxDate { get; set; }
        //public string cancelledBy { get; set; }



    }
    
}

