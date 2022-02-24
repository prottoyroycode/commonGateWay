using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayRequestAndResponse
{
    public class BLCancelSubscriptionDto
    {
         [JsonIgnore]
        public string company { get; set; }
        [Required]
        public string msisdn { get; set; }
        [Required]
        public string subRoot { get; set; }
        [Required]
        public string channel { get; set; }
    }
}
