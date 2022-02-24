using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class createandexecuteagrelationshipdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                column: "bkashExecuteAgreementRequestId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BkashExecuteAgreementResponse_BkashExecuteAgreementRequest_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                column: "bkashExecuteAgreementRequestId",
                principalTable: "BkashExecuteAgreementRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementResponse_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BkashExecuteAgreementResponse_BkashExecuteAgreementRequest_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse");

            migrationBuilder.DropIndex(
                name: "IX_BkashExecuteAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse");

            migrationBuilder.DropIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropColumn(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse");

            migrationBuilder.DropColumn(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");
        }
    }
}
