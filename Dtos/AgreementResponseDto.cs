using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Dtos
{
    public class AgreementResponseDto
    {
        public string bkashURL { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
