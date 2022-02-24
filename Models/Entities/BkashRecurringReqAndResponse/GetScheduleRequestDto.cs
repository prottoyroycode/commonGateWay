using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class GetScheduleRequestDto
    {
        public string freequency { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
