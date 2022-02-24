using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Core.User
{
    public interface IUserAccount
    {
        Task<ServiceResponse<ApplicationUserDto>> CreateUserAsync(ApplicationUserDto applicationUser);
    }
}
