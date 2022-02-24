using Bkash_Service_API.Core;
using Bkash_Service_API.Data;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bkash_Service_API.Controllers
{
    public class AuthController:BaseApiController
    {
        public readonly IAuthRepository _authRepository;
       
        public readonly IApplicationUserRepository _userAccount;
        public AuthController(IAuthRepository authRepository, IApplicationUserRepository userAccount)
        {
            _authRepository = authRepository;
            _userAccount = userAccount;
            
        }
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] ApplicationUserNameAndPassDto user)
        {
            var response = new CommonResponse();
            var res = new ServiceResponse<ApplicationUserNameAndPassDto>();
            if (!ModelState.IsValid)
            {
                res.Data = null;
                res.Message = string.Join(" and ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)); ;
                res.Status = false;
                return Ok(res);
            }
            var userCheck = await _userAccount.GetUserByKeyAndSecret(user);
            user.Id = userCheck.Id;
            user.Username = userCheck.UserName;
            if (userCheck == null)

                return BadRequest();
            var data = _authRepository.CreateToken(user,userCheck);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]

        [HttpGet("authStatus")]
        public IActionResult NoAuth()
        {
            var id = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok("done");
        }
        //  [AllowAnonymous]
        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUserDto applicationUser)
        {
            var data = await _userAccount.CreateUserAsync(applicationUser);
            return Ok(data);
        }
    }
}
