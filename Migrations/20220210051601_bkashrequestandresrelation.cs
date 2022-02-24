using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class bkashrequestandresrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestID",
                table: "CreateSubscriptionResponse_Bkash",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CreateSubscriptionResponse_Bkash_RequestID",
                table: "CreateSubscriptionResponse_Bkash",
                column: "RequestID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreateSubscriptionResponse_Bkash_CreateSubscriptionInitiate_Bkash_RequestID",
                table: "CreateSubscriptionResponse_Bkash",
                column: "RequestID",
                principalTable: "CreateSubscriptionInitiate_Bkash",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateSubscriptionResponse_Bkash_CreateSubscriptionInitiate_Bkash_RequestID",
                table: "CreateSubscriptionResponse_Bkash");

            migrationBuilder.DropIndex(
                name: "IX_CreateSubscriptionResponse_Bkash_RequestID",
                table: "CreateSubscriptionResponse_Bkash");

            migrationBuilder.DropColumn(
                name: "RequestID",
                table: "CreateSubscriptionResponse_Bkash");
        }
    }
}
