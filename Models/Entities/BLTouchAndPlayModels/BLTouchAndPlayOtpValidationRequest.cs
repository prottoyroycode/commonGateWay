using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayModels
{
    [Table("BLTouchAndPlayOtpValidationRequest")]
    public class BLTouchAndPlayOtpValidationRequest:BaseEntity
    {

        public string company { get; set; }

        public string msisdn { get; set; }

        public string subGroupId { get; set; }

        public string subRoot { get; set; }

        public string otp { get; set; }

        public string serviceId { get; set; }
        public BLTouchAndPlayOtpValidationResponse BLTouchAndPlayOtpValidationResponse { get; set; }
    }
}
