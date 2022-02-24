using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class addexecagresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BkashExecuteAgreementResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerMsisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trxID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BkashExecuteAgreementResponse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BkashExecuteAgreementResponse");
        }
    }
}
