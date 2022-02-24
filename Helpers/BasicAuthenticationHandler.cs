using Bkash_Service_API.Core;
using Bkash_Service_API.Data;
using Bkash_Service_API.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Bkash_Service_API.Helpers
{
    public class BasicAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
         private readonly IApplicationUserRepository _applicationUserRepository;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApplicationUserRepository applicationUserRepository
            ) :  base(options, logger, encoder, clock )
        {
            _applicationUserRepository = applicationUserRepository;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync( )
        {

            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
               
                return AuthenticateResult.Fail("Missing Authorization Header");

            //var user = new
            //{
            //    username = "pro",
            //    password = "pro123"
            //};
            ApplicationUser user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _applicationUserRepository.GetUserByUserNameAndPassword(username, password);
                
            }
            catch
            {
                
             
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");
            
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,user.Password.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            var token  = AuthenticateResult.Success(ticket);
            token.Properties.ExpiresUtc = System.DateTime.Now.AddMinutes(15);
            
            return token;
        }
    }
}
