using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class onetoonewithcareqandres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BkashCreateRequestId",
                table: "BkashCreateAgreementResponse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BkashCreateAgreementResponse_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse",
                column: "BkashCreateRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BkashCreateAgreementResponse_BkashCreateAgreementRequest_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse",
                column: "BkashCreateRequestId",
                principalTable: "BkashCreateAgreementRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BkashCreateAgreementResponse_BkashCreateAgreementRequest_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropIndex(
                name: "IX_BkashCreateAgreementResponse_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropColumn(
                name: "BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");
        }
    }
}
