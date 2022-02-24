using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class invoicenumbercolincareqable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "clientFailure_redirectURL",
                table: "BkashCreateAgreementRequest",
                newName: "client_invoiceNUmber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "client_invoiceNUmber",
                table: "BkashCreateAgreementRequest",
                newName: "clientFailure_redirectURL");
        }
    }
}
