using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Entities
{
    public class ApplicationUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Role { get; set; }
        
    }
}
