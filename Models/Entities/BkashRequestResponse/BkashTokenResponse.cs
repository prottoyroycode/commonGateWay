using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class BkashTokenResponse
    {
        public string id_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; } 
    }
}
