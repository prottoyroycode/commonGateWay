using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayModels
{
    [Table("BLTouchAndPlayOtpValidationResponse")]
    public class BLTouchAndPlayOtpValidationResponse:BaseEntity
    {
       public string responseString { get; set; }
       public string status { get; set; }
       public string message { get; set; }
       public string requestId { get; set; }
        [JsonProperty("Id")]
       public string user { get; set; }
       public DateTime CreatedAt { get; set; }
       public string UpdatedAt { get; set; }
        public Guid BLTouchAndPlayOtpvalidationRequestId { get; set; }
        public BLTouchAndPlayOtpValidationRequest BLTouchAndPlayOtpValidationRequest { get; set; }

    }
}
