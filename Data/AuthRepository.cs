using AutoMapper;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bkash_Service_API.Data
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
    }
    public class TokenResponse
    {
        public string token { get; set; }
        public DateTime ExpireTime { get; set; }
    }

    public class AuthRepository:IAuthRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthRepository(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
        }
        public CommonResponse CreateToken(ApplicationUserNameAndPassDto user ,ApplicationUser applicationUser)
        {
           
            var response = new CommonResponse();
            var tokenResponse = new TokenResponse();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role,applicationUser.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            key.KeyId = "love_you_pro";
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddMinutes(15),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokendDescriptor);
            var tokenData = tokenHandler.WriteToken(token);
            tokenResponse.token = tokenData;
            tokenResponse.ExpireTime =tokendDescriptor.Expires.Value;
            response.Data = tokenResponse;
            response.Message = "your token to make secure api call";
            response.StatusCode = ResStatusCode.Success;
            response.TotalRecords = 1;
            return response;
            //return tokenHandler.WriteToken(token);
           // return string.Empty;
        }

        
    }
}
