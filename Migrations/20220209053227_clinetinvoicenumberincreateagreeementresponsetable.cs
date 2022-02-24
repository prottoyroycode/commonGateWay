using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class clinetinvoicenumberincreateagreeementresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_invoiceNUmber",
                table: "BkashCreateAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_invoiceNUmber",
                table: "BkashCreateAgreementResponse");
        }
    }
}
