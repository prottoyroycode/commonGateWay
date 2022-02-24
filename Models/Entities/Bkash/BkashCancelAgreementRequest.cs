using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashCancelAgreementResponse")]
    public class BkashCancelAgreementResponse:BaseEntity
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string payerReference { get; set; }
        public string agreementVoidTime { get; set; }
        public string agreementStatus { get; set; }
        

    }
}
