using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class createandexecuteagrelationshipdonemodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementResponse_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropColumn(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.AddColumn<Guid>(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BkashCreateAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse",
                column: "bkashExecuteAgreementRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BkashCreateAgreementResponse_BkashExecuteAgreementRequest_bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse",
                column: "bkashExecuteAgreementRequestId",
                principalTable: "BkashExecuteAgreementRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BkashCreateAgreementResponse_BkashExecuteAgreementRequest_bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropIndex(
                name: "IX_BkashCreateAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropColumn(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.AddColumn<Guid>(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementResponseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementResponse_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementResponseId",
                principalTable: "BkashCreateAgreementResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
