using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class urlcolincaresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clientSuccess_redirectURL",
                table: "BkashCreateAgreementResponse",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clientSuccess_redirectURL",
                table: "BkashCreateAgreementResponse");
        }
    }
}
