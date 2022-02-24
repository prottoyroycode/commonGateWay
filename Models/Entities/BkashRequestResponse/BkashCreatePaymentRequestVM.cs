using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashCreatePaymentRequestVM
    {
        public string amount { get; set; }
        public string agreementId { get; set; }
        [JsonIgnore]
        public string mode { get; set; } = "0001";
        [JsonIgnore]
        public string callbackURL { get; set; }
        [JsonIgnore]
        public string currency { get; set; } = "BDT";
        [JsonIgnore]
        public string intent { get; set; } = "sale";
        [JsonIgnore]
        public string merchantInvoiceNumber { get; set; } = "inv-1";
    }
}
