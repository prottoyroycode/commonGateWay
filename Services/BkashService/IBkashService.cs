using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities.Bkash;
using Bkash_Service_API.Models.Entities.BkashRequestResponse;
using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BkashService
{
    public interface IBkashService
    {
        BkashTokenResponse GenerateToken();
        Task<ServiceResponse<AgreementResponseDto>> CreateAgreement2(string userId, string clientSuccess_redirectURL, string client_invoiceNUmber);
        Task<bKashCreateAgreementResponseVM> CreateAgreement(string userId,string clientSuccess_redirectURL ,string client_invoiceNUmber);
        Task<CommonResponse> ExecuteAgreement(string paymentID);
        Task<ServiceResponse<BkashExecuteAgreementResponse>> ExecuteAgreement2(string paymentID);
        Task<ServiceResponse<BkashExecuteAgreementResponse>> ExecuteAgreement3(string paymentID);
        Task<ServiceResponse<BkashAgreementStatusVM>> CheckAgreementStatusByAgreementId(string agreementID);
        BkashTokenResponse GenerateTokenForPaymentWithAgreement();
        Task<ServiceResponse<object>> CreatePaymentWithAgreementId(BkashCreatePaymentRequestVM bkashCreatePaymentRequestVM);
        Task<CommonResponse> ExecutePayment(string paymentID);
        Task<ServiceResponse<BkashCreateAgreementRequest>> GetReport();
        Task<BkashCreateAgreementRequest> GetClientUrl(string paymentId);
        Task<BkashExecuteAgreementResponse> GetAgreementIdByPaymentId(string paymentID);
        Task<BkashUserClientUrlAndInvoiceDto> GetClientURLByUserId(string paymentID);
        Task<bool> CheckUserExist(string userId);
        Task<ServiceResponse<List<UserAgreementsDto>>> GetAgreementIDByUserID(string userId);

        #region cancel_Agreement
        Task<BkashTokenResponse> GenerateTokenForAgreementCancel();
        Task<ServiceResponse<BkashCancelAgreementDto>> BkashCancelAgreement(string agreementID);

        #endregion cancel_Agreement

    }
}
