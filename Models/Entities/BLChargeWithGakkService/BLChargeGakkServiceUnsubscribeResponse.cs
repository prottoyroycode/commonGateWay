using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLChargeWithGakkService
{
    public class BLChargeGakkServiceUnsubscribeResponse:BaseEntity
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string msisdn { get; set; }
        public string service { get; set; }
        public string requestId { get; set; }
    }
}
