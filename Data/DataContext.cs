using Bkash_Service_API.Models.Entities;
using Bkash_Service_API.Models.Entities.Bkash;
using Bkash_Service_API.Models.Entities.BkashRecurringModels;
using Bkash_Service_API.Models.Entities.BLChargeWithGakkService;
using Bkash_Service_API.Models.Entities.BLTouchAndPlayModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        #region dbset
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BkashCreateAgreementRequest> BkashCreateAgreementRequests { get; set; }
        public DbSet<BkashCreateAgreementResponse> BkashCreateAgreementResponses { get; set; }
        public DbSet<BkashExecuteAgreementRequest> BkashExecuteAgreementRequests { get; set; }
        public DbSet<BkashExecuteAgreementResponse> BkashExecuteAgreementResponses { get; set; }
        public DbSet<BKashAgreementException> BKashAgreementExceptions { get; set; }
        public DbSet<BkashCreatePaymentWithAgreementRequest> BkashCreatePaymentWithAgreementRequests { get; set; }
        public DbSet<BkashCreatePaymentWithAgreementResponse> BkashCreatePaymentWithAgreementResponses { get; set; }
        public DbSet<BkashExecutePaymentWithAgreementRequest> BkashExecutePaymentWithAgreementRequests { get; set; }
        public DbSet<BkashExecutePaymentWithAgreementResponse> BkashExecutePaymentWithAgreementResponses { get; set; }
        public DbSet<BkashCancelAgreementResponse> BkashCancelAgreementResponse { get; set; }
        //bkash_sub
        public DbSet<CreateSubscriptionInitiateBkash> CreateSubscriptionInitiateBkashes { get; set; }
        public DbSet<CreateSubscriptionResponse> CreateSubscriptionResponses { get; set; }
        public DbSet<CreateSubscriptionCalBack> CreateSubscriptionCalBacks { get; set; }
        public DbSet<RecurringWebhookNotification> recurringWebhookNotifications { get; set; }

        //bkash_sub
        //bl_touchAnd_Play
        public DbSet<BLTouchAndPlayOtpRequest> BLTouchAndPlayOtpRequests { get; set; }
        public DbSet<BLTouchAndPlayOtpResponse> BLTouchAndPlayOtpResponses { get; set; }
        public DbSet<BLTouchAndPlayOtpValidationRequest> BLTouchAndPlayOtpValidationRequests { get; set; }
        public DbSet<BLTouchAndPlayOtpValidationResponse> BLTouchAndPlayOtpValidationResponses { get; set; }
        public DbSet<BLTouchAndPlayCacelSubscriptionRequest> BLTouchAndPlayCacelSubscriptionRequests { get; set; }
        public DbSet<BLTouchAndPlayCacelSubscriptionResponse> BLTouchAndPlayCacelSubscriptionResponses { get; set; }
        //bl_touchAnd_Play



        // bl_charging_using_gakk_service

        public DbSet<BLChargeGakkServiceDefination> BLChargeGakkServiceDefinations { get; set; }
        public DbSet<BLChargeGakkServiceDetail> BLChargeGakkServiceDetails { get; set; }
        public DbSet<BLChargeGakkServiceChargeRequest> BLChargeGakkServiceChargeRequests { get; set; }
        public DbSet<BLChargeGakkServiceChargeResponse> BLChargeGakkServiceChargeResponses { get; set; }
        public DbSet<BLChargeGakkServiceUnsubscribeRequest> BLChargeGakkServiceUnsubscribeRequests { get; set; }
        public DbSet<BLChargeGakkServiceUnsubscribeResponse> BLChargeGakkServiceUnsubscribeResponses { get; set; }
        #endregion dbset

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");

            modelBuilder.Entity<BLTouchAndPlayOtpRequest>()
           .HasOne<BLTouchAndPlayOtpResponse>(s => s.BLTouchAndPlayResponse)
           .WithOne(ad => ad.BLTouchAndPlayOtpRequest)
           .HasForeignKey<BLTouchAndPlayOtpResponse>(ad => ad.BLTouchAndPlayOtpRequestId);

            modelBuilder.Entity<BLTouchAndPlayOtpValidationRequest>()
           .HasOne<BLTouchAndPlayOtpValidationResponse>(s => s.BLTouchAndPlayOtpValidationResponse)
           .WithOne(ad => ad.BLTouchAndPlayOtpValidationRequest)
           .HasForeignKey<BLTouchAndPlayOtpValidationResponse>(ad => ad.BLTouchAndPlayOtpvalidationRequestId);

            modelBuilder.Entity<BLTouchAndPlayCacelSubscriptionRequest>()
           .HasOne<BLTouchAndPlayCacelSubscriptionResponse>(s => s.BLTouchAndPlayCacelSubscriptionResponse)
           .WithOne(ad => ad.BLTouchAndPlayCacelSubscriptionRequest)
           .HasForeignKey<BLTouchAndPlayCacelSubscriptionResponse>(ad => ad.CancelSubRequestId);

           modelBuilder.Entity<CreateSubscriptionInitiateBkash>()
          .HasOne<CreateSubscriptionResponse>(s => s.CreateSubscriptionResponse)
          .WithOne(ad => ad.request)
          .HasForeignKey<CreateSubscriptionResponse>(ad => ad.RequestID);

            // modelBuilder.Entity<BkashCreateAgreementRequest>()
            //.HasOne<BkashCreateAgreementResponse>(s => s.bkashCreateResponse)
            //.WithOne(ad => ad.bkashCreateRequest)
            //.HasForeignKey<BkashCreateAgreementResponse>(ad => ad.BkashCreateRequestId);








            base.OnModelCreating(modelBuilder);
        }
    }
}
