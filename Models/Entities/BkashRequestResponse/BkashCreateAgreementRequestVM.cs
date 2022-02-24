using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashCreateAgreementRequestVM
    {
        [Required]
        public string clientSuccess_redirectURL { get; set; }
        // public string clientFailure_redirectURL { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string client_invoiceNUmber { get; set; }
    }

    

}
