using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class removetablebkashapaymentresponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BkashCreatePaymentWithAgreementResponse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BkashCreatePaymentWithAgreementResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_BkashCreatePaymentWithAgreementResponse", x => x.Id);
                });
        }
    }
}
