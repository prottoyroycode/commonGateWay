﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class rmvtabletouch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLTouchAndPlayResponse");

            migrationBuilder.DropTable(
                name: "BLTouchAndPlayOtpRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayOtpRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Msisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionGroupID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayOtpRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayResponse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BLTouchAndPlayOtpRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTPString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BLTouchAndPlayResponse_BLTouchAndPlayOtpRequest_BLTouchAndPlayOtpRequestId",
                        column: x => x.BLTouchAndPlayOtpRequestId,
                        principalTable: "BLTouchAndPlayOtpRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLTouchAndPlayResponse_BLTouchAndPlayOtpRequestId",
                table: "BLTouchAndPlayResponse",
                column: "BLTouchAndPlayOtpRequestId",
                unique: true);
        }
    }
}
