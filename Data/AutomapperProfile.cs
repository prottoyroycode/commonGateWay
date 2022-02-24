using AutoMapper;
using Bkash_Service_API.Dtos;
using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.Bkash;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BkashRecurringReqAndResponse;
using Bkash_Service_API.Models.Entities.BkashRequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Data
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<AgreementResponseDto, BkashCreateAgreementResponse>();
            CreateMap<BkashCreateAgreementResponse, AgreementResponseDto>();
            CreateMap<BkashCreatePaymentWithAgreementResponse, PaymentResponseDto>();
            CreateMap<PaymentResponseDto, BkashCreatePaymentWithAgreementResponse>();
            CreateMap<BkashCancelAgreementResponse, BkashCancelAgreementDto>();
            CreateMap<BkashExecuteAgreementResponse, UserAgreementsDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.payerReference));
            CreateMap<RecurringWebhookNotification, RecurringWebhookNotificationDto>();
            CreateMap<RecurringWebhookNotificationDto, RecurringWebhookNotification>();
            CreateMap<CreateSubscriptionInitiateBkash, CreateSubscriptionInitiateBkashDto>();
            CreateMap<CreateSubscriptionInitiateBkashDto, CreateSubscriptionInitiateBkash>();
            





        }
    }
}
