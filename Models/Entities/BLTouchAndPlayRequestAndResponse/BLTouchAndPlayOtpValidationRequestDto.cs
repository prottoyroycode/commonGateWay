using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse
{
    public class BLTouchAndPlayOtpValidationRequestDto
    {
        //[Required]
        //public string company { get; set; }
        [Required]
        public string msisdn { get; set; }
        [Required]
        public string SubscriptionGroupID { get; set; }
        [Required]
        public string subRoot { get; set; }
        [Required]
        public string otp { get; set; }
        [Required]
        public string serviceId { get; set; }
    }
}
