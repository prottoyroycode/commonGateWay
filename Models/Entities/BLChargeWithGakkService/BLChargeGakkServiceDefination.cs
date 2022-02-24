using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLChargeWithGakkService
{
    public class BLChargeGakkServiceDefination
    {

        public int Id { get; set; }
        public string Purpose { get; set; }
        public string ChargeCode { get; set; }
        public string CpName { get; set; }
        public string ShortCode { get; set; }
        public decimal ChargeBtdVatSd { get; set; }
    }
}
