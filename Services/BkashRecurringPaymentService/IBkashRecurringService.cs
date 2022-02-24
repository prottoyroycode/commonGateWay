using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse;
using Bkash_Service_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Services.BkashRecurringPaymentService
{
    public interface IBkashRecurringService
    {
        Task<ServiceResponse<RecurringWebhookNotificationDto>> BkashWebhookNotificationAsync(RecurringWebhookNotificationDto recurringWebhookNotificationDto);
        Task<ServiceResponse<CreateSubscriptionResponseDto>> BkashRecurringCreateSubAsync(CreateSubscriptionInitiateBkashDto model);
        Task<ServiceResponse<BkashQuerySubscriptionAndPaymentDto>> BkashQuerySubscriptionAndPaymentAfterCallBackAsync(string subscriptionRequestId);
        Task<ServiceResponse<CancelSubscriptionResponseDto>> CancelSubscriptionAsync(int subscriptionId);
        Task<ServiceResponse<List<GetPaymentBySubscriptionIdResponseDto>>> GetPaymentListBySubscriptionIdAsync(int subscriptionRequestId);
        Task<ServiceResponse<GetPaymentInfoByPaymentIdResponseDto>> GetPaymentInfoByPaymentId(int paymentId);
        Task<ServiceResponse<RefundErrorResponseDto>> RefundPaymentAsync(string paymentId ,string amount);
        Task<ServiceResponse<QueryBySubscriptionIdResponseDto>> QueryBySubscriptionId(int subscriptionID);
        Task<ServiceResponse<SubscriptionListResponseDto>> SubscriptionList(int pageNumber ,int size);
        Task<ServiceResponse<GetScheduleResponseDto>> GetPaymentScheduleUrl(string freequency,string startDate,string endDate);
        Task<bool> CheckIfUserExists(string userId);
        Task<ServiceResponse<UserSubscriptionStatusDto>> CheckUsersSubscriptionStatus(string userId); 

        //Task<ServiceResponse<object>> StoreCreatesResponseAsync();
        //   Task<ServiceResponse<object>> CreateBkashSubscriptionAsync();
        // Task<ServiceResponse<CreateSubscriptionResponse>> BkashCreateSubscription();
    }
}
