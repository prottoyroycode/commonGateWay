﻿// <auto-generated />
using System;
using Bkash_Service_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bkash_Service_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220131062457_clientuseridcolumninbaseentity")]
    partial class clientuseridcolumninbaseentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApiSecret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BKashAgreementException", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trxID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BKashAgreementException");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashCancelAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementVoidTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashCancelAgreementResponse");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashCreateAgreementRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MerchantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("clientSuccess_redirectURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_invoiceNUmber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerReference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashCreateAgreementRequest");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashCreateAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bkashURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cancelledCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("clientSuccess_redirectURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("failureCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentCreateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("successCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashCreateAgreementResponse");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashCreatePaymentWithAgreementRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashCreatePaymentWithAgreementRequest");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashCreatePaymentWithAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bkashURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cancelledCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("failureCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentCreateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("successCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashCreatePaymentWithAgreementResponse");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashExecuteAgreementRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashExecuteAgreementRequest");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashExecuteAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementExecuteTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerMsisdn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentExecuteTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trxID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashExecuteAgreementResponse");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashExecutePaymentWithAgreementRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashExecutePaymentWithAgreementRequest");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.Bkash.BkashExecutePaymentWithAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerMsisdn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentExecuteTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trxID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BkashExecutePaymentWithAgreementResponse");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.BkashRecurringModels.CreateSubscriptionInitiateBkash", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("expiryDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("firstPaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("firstPaymentIncludedInCycle")
                        .HasColumnType("bit");

                    b.Property<string>("frequecy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("maxCapAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("merchantShortCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("redirectURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.Property<string>("startDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subscriptionReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subscriptionRequestId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subscriptionType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreateSubscriptionInitiate_Bkash");
                });

            modelBuilder.Entity("Bkash_Service_API.Models.Entities.BkashRecurringModels.RecurringWebhookNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("client_UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dueDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("firstPayment")
                        .HasColumnType("bit");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nextPaymentDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("paymentId")
                        .HasColumnType("int");

                    b.Property<string>("paymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("subscriptionRequestId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timeStamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("trxDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("trxId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RecurringWebhookNotification");
                });
#pragma warning restore 612, 618
        }
    }
}
