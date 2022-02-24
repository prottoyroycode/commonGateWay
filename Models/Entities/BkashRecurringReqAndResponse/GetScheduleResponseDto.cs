using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse
{
    public class GetScheduleResponseDto
    {
        public int count { get; set; }
        public List<string> dates { get; set; }
    }

}
