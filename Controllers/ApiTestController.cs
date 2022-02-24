using AutoMapper;
using Bkash_Service_API.Core;
using Bkash_Service_API.Core.User;
using Bkash_Service_API.Data;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.Bkash;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRequestResponse;
using Bkash_Service_API.Models.Response;
using Bkash_Service_API.Services.BkashService;
using CSharpCornerAuthDemo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bkash_Service_API.Controllers
{

    public class ApiTestController : BaseApiController
    {
        public readonly IAuthRepository _authRepository;

        public readonly IApplicationUserRepository _userAccount;
        public readonly IBkashService _bkashService;
       
        public readonly IMapper _mapper;
        public ApiTestController(IAuthRepository authRepository, IApplicationUserRepository userAccount, IBkashService bkashService,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _userAccount = userAccount;
            _mapper = mapper;
            _bkashService = bkashService;
            
        }
        //auth must
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
            var data = _authRepository.CreateToken(user, userCheck);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]

        [HttpGet("noauth")]
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
        //auth must
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("bkashToken")]
        public IActionResult BkashTokenGeneerate()
        {
            var token = _bkashService.GenerateToken();
            return Ok(token);
        }
        //auth must with role
        [Authorize(AuthenticationSchemes ="Bearer" ,Roles =Role.TokenizedService_Bkash) ]
        [HttpPost("createAgreement")]
        public async Task<IActionResult> CreateAgreementReq([FromBody] BkashCreateAgreementRequestVM bkashCreateAgreementRequestVM)
        {
            //var response = new CommonResponse();
            var res = new ServiceResponse<BkashCreateAgreementRequestVM>();
            if (!ModelState.IsValid)
            {
                res.Data = null;
                res.Message = string.Join(" and ", ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)); ;
                res.Status = false;
                return Ok(res);
            }

            var checkUserId = await _bkashService.CheckUserExist(bkashCreateAgreementRequestVM.UserId);
            if (checkUserId)
            {

                //var userDataMap = new BkashCreateAgreementRequestVM
                //{
                //    UserId = bkashCreateAgreementRequestVM.UserId,

                //};

                res.Data = null;
                res.Status = false;
                res.Message = "user already exists";
                res.TotalRecords = 1;
                res.StatusCode = ResStatusCode.BadRequest;
                return Ok(res);
            }

            // return BadRequest(new { Message ="user already exist",status =false } );;
            var data = await _bkashService.CreateAgreement2(bkashCreateAgreementRequestVM.UserId,
            bkashCreateAgreementRequestVM.clientSuccess_redirectURL, bkashCreateAgreementRequestVM.client_invoiceNUmber);
            return Ok(data);
        }
        [HttpGet("agreementReport")]
        public async Task<ActionResult<ServiceResponse<BkashCreateAgreementRequest>>> AgreementReport()
        {
            var result = await _bkashService.GetReport();
            return Ok(result);
        }
        [HttpPost("test-createAgreement3")]
        public async Task<IActionResult> CreateAgreement3Test()
        {
            var paymentID = "TR0000NF1642047789206";
            var data = await _bkashService.ExecuteAgreement3(paymentID);
            return Ok(data);
        }
        [HttpGet("transaction")]
        public async Task<IActionResult> BkashCallBackOnCreateAgreementSuccess(string paymentID, string status, string? apiVersion)
        {
            //  var urlData = await _bkashService.GetClientUrl(paymentID);
            // var url = urlData.clientSuccess_redirectURL;
            if (status == "success")
            {
                  var data = await _bkashService.ExecuteAgreement3(paymentID);
                //if(data == null)
                //{
                //    await Task.Delay(7000);
                //}
                
                if (data.Data != null)
               
                {
                     var agreementId = data.Data.agreementID;
                    var msisdn = data.Data.customerMsisdn;
                   
                   // var agreementId = await _bkashService.GetAgreementIdByPaymentId(paymentID);

                    var clientURL = await _bkashService.GetClientURLByUserId(paymentID);
                    if (clientURL != null)
                    {
                        return Redirect(clientURL.clientSuccess_redirectURL + '/' + agreementId + '/' + status +'/'+clientURL.client_invoiceNUmber+ '/'+msisdn);
                    }
                    var url = "https://partnersintregation.techapi24.com";
                    if (clientURL == null)
                    {
                        return Redirect(url+ '/' + agreementId + '/' + status + '/');
                    }

                   
                    //if(clientURL ==null || clientURL =="")

                    return Ok("client url not found");


                }
                else
                {
                    // var agreementId = await _bkashService.GetAgreementIdByPaymentId(paymentID);
                    var clientURL = await _bkashService.GetClientURLByUserId(paymentID);
                    return Redirect(clientURL.clientSuccess_redirectURL + '/' + status + " ");
                    // return Ok();
                }
            }
            else
            {
                var clientURL = await _bkashService.GetClientURLByUserId(paymentID);
                if (clientURL != null)

                    return Redirect(clientURL.clientSuccess_redirectURL + '/' + paymentID + '/' + status);
            }


            return Ok(new
            {
                message = "failed",
                status = false
            }); ;
            ;

            //if (status == "success")
            //{

            //}
            // return Ok(data);






        }
        //auth must with role
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Role.TokenizedService_Bkash)]
        [HttpGet("agreementStatus")]
        public async Task<IActionResult> CheckAGreementStatus(string agreementId)
        {
            var data = await _bkashService.CheckAgreementStatusByAgreementId(agreementId);
            return Ok(data);
        }

        [HttpPost("grantToken_payment")]
        public IActionResult GrantToken()
        {
            var data = _bkashService.GenerateTokenForPaymentWithAgreement();
            return Ok(data);
        }
        //auth must with role
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Role.TokenizedService_Bkash)]
        [HttpPost("createPayment")]
        public async Task<IActionResult> CreatePaymentRequst(BkashCreatePaymentRequestVM bkashCreatePaymentRequestVM)

        {
            var data = await _bkashService.CreatePaymentWithAgreementId(bkashCreatePaymentRequestVM);
            return Ok(data);
        }
        [HttpPost("testsamepaymentid")]
        public async Task<IActionResult> TestPaymentid()
        {
            var paymentID = "TR0001X41642667739894";
            var data = await _bkashService.ExecutePayment(paymentID);
            return Ok(data);
        }
        [HttpGet("paymentCallback")]
        public async Task<IActionResult> PaymentCallBackFromBkash(string paymentID, string status, string apiVersion)
        {
            //  paymentID = "TR0001X41642667739894";
            var data = await _bkashService.ExecutePayment(paymentID);
            if (data.Status == true)
                return Ok("success");
            return Ok("failed");
        }

        [HttpPost("cancel_agreement_token")]
        public async Task<IActionResult> CancelAgreementToken()
        {
            var data = await _bkashService.GenerateTokenForAgreementCancel();
            return Ok(data);
        }
        //auth must with role
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Role.TokenizedService_Bkash)]
        [HttpPost("cancelAgreement")]
        public async Task<IActionResult> CancelAgreement([FromBody] BkashCancelAgreementRequestVM bkashCancelAgreementRequestVM)
        {
            var data = await _bkashService.BkashCancelAgreement(bkashCancelAgreementRequestVM.agreementID);
            return Ok(data);
        }
        //auth must with role
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Role.TokenizedService_Bkash)]
        [HttpPost("user_agreementID")]
        public async Task<IActionResult> GetUserAgreementId([FromBody] UserAgreementIdDto userAgreementIdDto)
        {
            var data = await _bkashService.GetAgreementIDByUserID(userAgreementIdDto.UserId);
            return Ok(data);
        }

        //subscription api
        //[HttpPost("subscription")]
        //public async Task<IActionResult> CreateSubscription()
        //{
        //    var data = await _bkashRecurringService.BkashCreateSubscription();
        //    return Ok(data);
        //}




    }
}
