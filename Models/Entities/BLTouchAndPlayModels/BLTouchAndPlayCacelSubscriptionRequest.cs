using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BLTouchAndPlayModels
{
    [Table("BLTouchAndPlayCacelSubscriptionRequest")]
    public class BLTouchAndPlayCacelSubscriptionRequest : BaseEntity
    {
        public string company { get; set; }

        public string msisdn { get; set; }


        public string subRoot { get; set; }

        public string channel { get; set; }
        public BLTouchAndPlayCacelSubscriptionResponse BLTouchAndPlayCacelSubscriptionResponse { get; set; }
    }
}
