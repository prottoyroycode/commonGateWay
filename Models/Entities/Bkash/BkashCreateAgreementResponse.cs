using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashCreateAgreementResponse")]
    public class BkashCreateAgreementResponse :BaseEntity
    {
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string paymentCreateTime { get; set; }
        public string transactionStatus { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string bkashURL { get; set; }
        public string callbackURL { get; set; }
        public string successCallbackURL { get; set; }
        public string failureCallbackURL { get; set; }
        public string cancelledCallbackURL { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string UserId { get; set; }
        public string clientSuccess_redirectURL { get; set; }
        public string client_invoiceNUmber { get; set; }
        //public Guid BkashCreateRequestId { get; set; }
        //public BkashCreateAgreementRequest bkashCreateRequest { get; set; }
        //public BkashExecuteAgreementRequest bkashExecuteAgreementRequest { get; set; }
    }
}
