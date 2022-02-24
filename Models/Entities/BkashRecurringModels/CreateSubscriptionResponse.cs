using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringModels
{
    [Table("CreateSubscriptionResponse_Bkash")]
    public class CreateSubscriptionResponse : BaseEntity
    {
        public string subscriptionRequestId { get; set; }
        public string redirectURL { get; set; }
        public string expirationTime { get; set; }
        public DateTime timeStamp { get; set; }
        public string errorCode { get; set; }
        public string errorDescription { get; set; }
        public string reference { get; set; }
        public string client_RedirectURL { get; set; }
        public Guid RequestID { get; set; }
        public CreateSubscriptionInitiateBkash request { get; set; }






    }
    
}
