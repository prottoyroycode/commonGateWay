using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BkashExecutePaymentWithAgreementRequest")]
    public class BkashExecutePaymentWithAgreementRequest:BaseEntity
    {
        public string paymentID { get; set; }

    }
}
