using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayModels
{
    [Table("BLTouchAndPlayResponse")]
    public class BLTouchAndPlayOtpResponse :BaseEntity
    {
        public string OTPString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        [JsonProperty("Id")]
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedAt { get; set; } 
        public Guid BLTouchAndPlayOtpRequestId { get; set; }
        public BLTouchAndPlayOtpRequest BLTouchAndPlayOtpRequest { get; set; }
    }
    
}
