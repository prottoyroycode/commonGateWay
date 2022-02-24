using Bkash_Service_API.Models.Entities.BkashRequestResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashCreateAgreementRequest")]
    public class BkashCreateAgreementRequest :BaseEntity
    {
        public BkashCreateAgreementRequest(string mode  ,string callbackURL,string payerReference)
        {
            this.mode = mode;
            this.callbackURL = callbackURL;
            this.payerReference = payerReference;
            
        }
        public string UserId { get; set; }
        public string MerchantId { get; set; }
        public string mode { get; set; }
        public string payerReference { get; set; }
        public string callbackURL { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string agreementID { get; set; }
        public string merchantInvoiceNumber { get; set; } 
        public string clientSuccess_redirectURL { get; set; }
        public string client_invoiceNUmber { get; set; }

      //  public BkashCreateAgreementResponse bkashCreateAgreementResponse { get; set; }
      //   public BkashCreateAgreementRequest bkashCreateAgreementRequest { get; set; }
      // public BkashCreateAgreementResponse BkashCreateAgreementResponse { get; set; }
    }
}
