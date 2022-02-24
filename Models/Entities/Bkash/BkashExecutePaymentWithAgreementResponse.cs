using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashExecutePaymentWithAgreementResponse")]
    public class BkashExecutePaymentWithAgreementResponse:BaseEntity
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string payerReference { get; set; }
        public string customerMsisdn { get; set; }
        public string trxID { get; set; }
        public string amount { get; set; }
        public string transactionStatus { get; set; }
        public string paymentExecuteTime { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
    }
}
