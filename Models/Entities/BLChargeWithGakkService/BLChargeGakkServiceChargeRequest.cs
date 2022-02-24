using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLChargeWithGakkService
{
    public class BLChargeGakkServiceChargeRequest:BaseEntity
    {
        public BLChargeGakkServiceChargeRequest()
        {
            CreatedDate = DateTime.Now;
        }

        public string ClientId { get; set; }
        public string GakkServiceId { get; set; }
        public string Msisdn { get; set; }
    }
}
