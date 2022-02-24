using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse
{
    public class BLTouchAndPlayOtpReqDto
    {
        [Required]
        public string Msisdn { get; set; }
        [Required]
        public string SubscriptionGroupID { get; set; }
    }


}
