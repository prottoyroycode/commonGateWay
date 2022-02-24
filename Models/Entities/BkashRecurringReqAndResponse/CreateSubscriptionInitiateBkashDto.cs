using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class CreateSubscriptionInitiateBkashDto
    {
        public decimal amount { get; set; }
        public string userId { get; set; }
        public string client_RedirectURL { get; set; }
        public string freequency { get; set; }
        


    }
}
