using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Dtos
{
    public class BkashCancelAgreementDto
    {
       // public string statusCode { get; set; }
        public string statusMessage { get; set; }
     //   public string paymentID { get; set; }
        public string agreementID { get; set; }
     //   public string payerReference { get; set; }
        public string agreementVoidTime { get; set; }
        public string agreementStatus { get; set; }
    }
}
