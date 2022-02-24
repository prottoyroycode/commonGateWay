using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashAgreementStatusVM
    {
        public string paymentID { get; set; }
        public string mode { get; set; }
        public string verificationStatus { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string payerReference { get; set; }
        public string agreementID { get; set; }
        public string agreementStatus { get; set; }
        public string agreementCreateTime { get; set; }
        public string agreementExecuteTime { get; set; }
    }
}
