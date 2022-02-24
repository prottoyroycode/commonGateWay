using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashCancelAgreementRequestVM
    {
        [Required]
        public string agreementID { get; set; }
    }
}
