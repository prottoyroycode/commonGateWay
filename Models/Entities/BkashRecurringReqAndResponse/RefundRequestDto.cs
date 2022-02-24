using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class RefundRequestDto
    {
        [Required]
        public string paymentId { get; set; }
        [Required]
        public string amount { get; set; }
    }
}
