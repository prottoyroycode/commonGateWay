using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLChargeWithGakkService
{
    public class BLChargeGakkServiceDetail
    {
        public int Id { get; set; }
        public int ServiceDefinationId { get; set; }
        public string GakkServiceId { get; set; }
        public string ServiceId { get; set; }
        public string SubscriptionGroupId { get; set; }
        public string SubscriptionRoot { get; set; }
        public string Sc { get; set; }
        public string CpId { get; set; }
        public string CpPass { get; set; }
        public string ServiceType { get; set; }
        public bool IsActive { get; set; }
        public int interval { get; set; }
    }
}
