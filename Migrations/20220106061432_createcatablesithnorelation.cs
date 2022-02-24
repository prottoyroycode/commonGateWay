using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class createcatablesithnorelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BkashCreateAgreementResponse_BkashCreateAgreementRequest_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementRequest_bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest");

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
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropIndex(
                name: "IX_BkashCreateAgreementResponse_BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");

            migrationBuilder.DropColumn(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse");

            migrationBuilder.DropColumn(
                name: "bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropColumn(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest");

            migrationBuilder.DropColumn(
                name: "BkashCreateRequestId",
                table: "BkashCreateAgreementResponse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BkashCreateRequestId",
                table: "BkashCreateAgreementResponse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementResponse_bkashExecuteAgreementRequestId",
                table: "BkashExecuteAgreementResponse",
                column: "bkashExecuteAgreementRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BkashExecuteAgreementRequest_bkashCreateAgreementResponseId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementResponseId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BkashExecuteAgreementRequest_BkashCreateAgreementRequest_bkashCreateAgreementRequestId",
                table: "BkashExecuteAgreementRequest",
                column: "bkashCreateAgreementRequestId",
                principalTable: "BkashCreateAgreementRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
