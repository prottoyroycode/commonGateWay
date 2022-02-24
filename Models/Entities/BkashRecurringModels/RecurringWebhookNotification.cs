using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringModels
{
    [Table("RecurringWebhookNotification")]
    public class RecurringWebhookNotification:BaseEntity
    {
        //public string message { get; set; }
        // public MessageVM message { get; set; }
        [NotMapped]
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
    }
    public class MessageVM
    {

    }
}

