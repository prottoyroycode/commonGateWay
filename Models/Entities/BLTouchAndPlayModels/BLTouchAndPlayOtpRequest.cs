using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayModels
{
    [Table("BLTouchAndPlayOtpRequest")]
    public class BLTouchAndPlayOtpRequest:BaseEntity
    {
        public string Company { get; set; }
        public string Msisdn { get; set; }
        public string SubscriptionGroupID { get; set; } 
        public BLTouchAndPlayOtpResponse BLTouchAndPlayResponse { get; set; }
    }
}
