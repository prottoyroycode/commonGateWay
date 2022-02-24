using AutoMapper;
using Bkash_Service_API.Core.User;
using Bkash_Service_API.Data;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Infrastructure.UserAccount
{
    public class UserAccount : IUserAccount
    {
        public readonly IUserAccount _userAccount;
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public UserAccount(IUserAccount userAccount , DataContext context , IMapper mapper)
        {
            _userAccount = userAccount;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ApplicationUserDto>> CreateUserAsync(ApplicationUserDto applicationUser )
        {
            
            var response = new ServiceResponse<ApplicationUserDto>();
            ApplicationUser userData = _mapper.Map<ApplicationUser>(applicationUser);
            try
            {

                var result = await _context.ApplicationUsers.AddAsync(userData);
                var isInserted =await _context.SaveChangesAsync();
                response.Data = applicationUser;
                response.Message = "user inserted successfully";
                response.Status = true;
                
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

            }
            return response;
        }

        
    }
}
