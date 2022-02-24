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
    [Migration("20220105084238_removetablebkashapaymentresponse")]
    partial class removetablebkashapaymentresponse
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

                    b.Property<string>("UserName")
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

                    b.Property<string>("agreementID")
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

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
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
#pragma warning restore 612, 618
        }
    }
}
