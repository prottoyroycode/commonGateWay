using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashUserClientUrlAndInvoiceDto
    {
        public string clientSuccess_redirectURL { get; set; }
        public string client_invoiceNUmber { get; set; }
        //public string msisdn { get; set; }
    }
}
