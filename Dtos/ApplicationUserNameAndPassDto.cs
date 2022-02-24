using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bkash_Service_API.Dtos
{
    public class ApplicationUserNameAndPassDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
        [Required]
        public string ApiKey { get; set; }
        [Required]
        public string ApiSecret { get; set; }
      //  public string Role { get; set; }
    }
}
