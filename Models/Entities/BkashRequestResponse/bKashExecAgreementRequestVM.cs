using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class bKashExecAgreementRequestVM
    {
        public string paymentID { get; set; }
        public string userId { get; set; }
       // public string client_userId { get; set; }
    }
    public class BkashExecutePaymentVM
    {
        public string paymentID { get; set; }
    }
    
}
