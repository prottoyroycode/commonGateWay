using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringModels
{
    [Table("CreateSubscriptionCalBack")]
    public class CreateSubscriptionCalBack :BaseEntity
    {
        public string reference { get; set; }
        public string status { get; set; }

    }
}
