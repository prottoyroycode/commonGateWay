using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashCreatePaymentWithAgreementRequest")]
    public class BkashCreatePaymentWithAgreementRequest:BaseEntity
    {
        public string mode { get; set; } = "0001";
        public string callbackURL { get; set; } 
        public string agreementID { get; set; } 
        public string amount { get; set; }
        public string currency { get; set; } = "BDT";
        public string intent { get; set; } = "sale";
        public string merchantInvoiceNumber { get; set; }
    }
}
