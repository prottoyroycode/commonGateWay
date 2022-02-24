using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringModels
{
    [Table("CreateSubscriptionInitiate_Bkash")]
    public class CreateSubscriptionInitiateBkash : BaseEntity
    {
        public string amount { get; set; }
        public string amountQueryUrl { get; set; } = null;
        public string firstPaymentAmount { get; set; } = null;
        public bool firstPaymentIncludedInCycle { get; set; } = true;
        public int serviceId { get; set; } = 100001;
        public string currency { get; set; } = "BDT";
        public string startDate { get; set; } = "2022-02-25";
        public string expiryDate { get; set; } = "2022-02-26";
        public string frequency { get; set; } = "DAILY";
        public string subscriptionType { get; set; } = "BASIC";
        public string maxCapAmount { get; set; } = null;
        public bool maxCapRequired { get; set; } = false;
        public int merchantShortCode { get; set; } = 50011;
        public string payer { get; set; } = null;
        public string payerType { get; set; } = "CUSTOMER";
        public string paymentType { get; set; } = "FIXED";
        public string redirectUrl { get; set; } = "https://localhost:44306/api/BkashRedirect";
        public string subscriptionRequestId { get; set; }
        public string subscriptionReference { get; set; }
        public CreateSubscriptionResponse CreateSubscriptionResponse { get; set; }



        //public string subscriptionRequestId { get; set; }
        //public int serviceId { get; set; }
        //public string redirectURL { get; set; }
        //public string paymentType { get; set; }
        //public string subscriptionType { get; set; }
        //public decimal amount { get; set; }
        //public decimal firstPaymentAmount { get; set; } //optional
        // public string currency { get; set; } = "BDT";
        // public bool firstPaymentIncludedInCycle { get; set; } = true; //optional
        //public decimal maxCapAmount { get; set; } //optional
        //public string frequecy { get; set; }
        //public string startDate { get; set; }
        //public string expiryDate { get; set; }
        //public string merchantShortCode { get; set; } //optional
        //public string payerType { get; set; } 
        //public string payer { get; set; }
        //public string subscriptionReference { get; set; }
        [NotMapped]
        public ExtraParams extraParams { get; set; }


    }
    public class ExtraParams : BaseEntity
    {

    }
}
