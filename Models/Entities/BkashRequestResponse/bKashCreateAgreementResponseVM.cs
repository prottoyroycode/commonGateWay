using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class bKashCreateAgreementResponseVM
    {
        public bKashCreateAgreementResponseVM()
        {
            this.bkashURL = null;
            this.statusCode = 2003.ToString();
            this.statusMessage = "Process failed";
        }
        public string bkashURL { get; set; }

        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}
