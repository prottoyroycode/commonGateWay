using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashExecuteAgreementRequest")]
    public class BkashExecuteAgreementRequest:BaseEntity
    {
        public string UserId { get; set; }
        public string paymentID { get; set; }
        //public Guid bkashCreateAgreementResponseId { get; set; }
        //public BkashCreateAgreementRequest bkashCreateAgreementRequest { get; set; } 
        //public BkashExecuteAgreementResponse bkashExecuteAgreementResponse { get; set; }
    }
}
