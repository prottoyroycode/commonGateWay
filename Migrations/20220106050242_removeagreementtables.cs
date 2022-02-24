using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class removeagreementtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BkashExecuteAgreementResponse");

            migrationBuilder.DropTable(
                name: "BkashExecuteAgreementRequest");

            migrationBuilder.DropTable(
                name: "BkashCreateAgreementResponse");

            migrationBuilder.DropTable(
                name: "BkashCreateAgreementRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BkashCreateAgreementRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MerchantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    callbackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BkashCreateAgreementRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BkashCreateAgreementResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BkashCreateRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bkashURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    callbackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cancelledCallbackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    failureCallbackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentCreateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    successCallbackURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BkashCreateAgreementResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BkashCreateAgreementResponse_BkashCreateAgreementRequest_BkashCreateRequestId",
                        column: x => x.BkashCreateRequestId,
                        principalTable: "BkashCreateAgreementRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BkashExecuteAgreementRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bkashCreateAgreementResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BkashExecuteAgreementRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementResponse_bkashCreateAgreementResponseId",
                        column: x => x.bkashCreateAgreementResponseId,
                        principalTable: "BkashCreateAgreementResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BkashExecuteAgreementResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bkashExecuteAgreementRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerMsisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trxID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BkashExecuteAgreementResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BkashExecuteAgreementResponse_BkashExecuteAgreementRequest_bkashExecuteAgreementRequestId",
                        column: x => x.bkashExecuteAgreementRequestId,
                        principalTable: "BkashExecuteAgreementRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BkashCreateAgreementResponse_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse",
                column: "BkashCreateRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementResponseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                column: "bkashExecuteAgreementRequestId",
                unique: true);
        }
    }
}
