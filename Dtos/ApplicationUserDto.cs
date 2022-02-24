using Bkash_Service_API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Dtos
{
    public class ApplicationUserDto 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; } 
       
    }
}
