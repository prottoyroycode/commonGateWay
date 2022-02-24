using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities.BkashRequestResponse
{
    public class TokenRequest
    {
        public string app_secret { get; set; }
        public string app_key { get; set; }

        public TokenRequest(string app_secret, string app_key)
        {
            this.app_secret = app_secret;
            this.app_key = app_key;
        }
    }
}
