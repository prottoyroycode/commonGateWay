using AutoMapper;
using Bkash_Service_API.Core;
using Bkash_Service_API.Data;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Infrastructure
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {

        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public ApplicationUserRepository(DataContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ApplicationUserDto>> CreateUserAsync(ApplicationUserDto applicationUser)
        {


            var response = new ServiceResponse<ApplicationUserDto>();
            var checkExistingUser = await GetUserByUserNameAndPassword(applicationUser.UserName, applicationUser.Password);
            if (checkExistingUser != null)
            {
                response.Status = false;
                response.Message = "user already exist";
                response.StatusCode = ResStatusCode.BadRequest;
                return response;
            }


            ApplicationUser userData = _mapper.Map<ApplicationUser>(applicationUser);
            userData.Role = "not assigned yet";
            userData.IsActive = true;

            try
            {

                var result = await _context.ApplicationUsers.AddAsync(userData);
                var isInserted = await _context.SaveChangesAsync();
                response.Data = applicationUser;
                response.Message = "user inserted successfully";
                response.Status = true;
                response.TotalRecords = isInserted;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

            }
            return response;
        }

        public async Task<ApplicationUser> GetUserByKeyAndSecret(ApplicationUserNameAndPassDto applicationUser)
        {

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.ApiKey == applicationUser.ApiKey && u.ApiSecret == applicationUser.ApiSecret);
            if (user == null)
                return null;
            return user;
        }

        public async Task<ApplicationUser> GetUserByUserNameAndPassword(string userName, string password)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Password == password && u.UserName == userName);
            if (user == null)
                return null;
            return user;
        }
    }
}
