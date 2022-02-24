using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLChargeWithGakkService
{
    public class BLChargeGakkServiceUnsubscribeRequest:BaseEntity
    {

        public BLChargeGakkServiceUnsubscribeRequest()
        {
            this.CreatedDate = DateTime.Now;
        }
        public string Msisdn { get; set; }
        public string GakkServiceId { get; set; }
    }
}
