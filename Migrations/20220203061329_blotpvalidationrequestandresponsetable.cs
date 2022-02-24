using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class blotpvalidationrequestandresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayOtpValidationRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    msisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayOtpValidationRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayOtpValidationResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    responseString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BLTouchAndPlayOtpvalidationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayOtpValidationResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BLTouchAndPlayOtpValidationResponse_BLTouchAndPlayOtpValidationRequest_BLTouchAndPlayOtpvalidationRequestId",
                        column: x => x.BLTouchAndPlayOtpvalidationRequestId,
                        principalTable: "BLTouchAndPlayOtpValidationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLTouchAndPlayOtpValidationResponse_BLTouchAndPlayOtpvalidationRequestId",
                table: "BLTouchAndPlayOtpValidationResponse",
                column: "BLTouchAndPlayOtpvalidationRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLTouchAndPlayOtpValidationResponse");

            migrationBuilder.DropTable(
                name: "BLTouchAndPlayOtpValidationRequest");
        }
    }
}
