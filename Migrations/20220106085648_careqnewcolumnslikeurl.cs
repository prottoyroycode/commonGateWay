using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class careqnewcolumnslikeurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clientFailure_redirectURL",
                table: "BkashCreateAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "clientSuccess_redirectURL",
                table: "BkashCreateAgreementRequest",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clientFailure_redirectURL",
                table: "BkashCreateAgreementRequest");

            migrationBuilder.DropColumn(
                name: "clientSuccess_redirectURL",
                table: "BkashCreateAgreementRequest");
        }
    }
}
