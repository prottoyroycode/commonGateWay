using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.Bkash
{
    [Table("BKashAgreementException")]
    public class BKashAgreementException:BaseEntity
    {
        public BKashAgreementException()
        {
            this.merchantInvoiceNumber = null;
            this.paymentID = null;
        }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string trxID { get; set; }
    }
}
