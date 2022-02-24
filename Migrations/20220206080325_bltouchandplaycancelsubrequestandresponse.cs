using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class bltouchandplaycancelsubrequestandresponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayCacelSubscriptionRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    msisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    channel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayCacelSubscriptionRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayCacelSubscriptionResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    responseString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancelSubRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayCacelSubscriptionResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BLTouchAndPlayCacelSubscriptionResponse_BLTouchAndPlayCacelSubscriptionRequest_CancelSubRequestId",
                        column: x => x.CancelSubRequestId,
                        principalTable: "BLTouchAndPlayCacelSubscriptionRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLTouchAndPlayCacelSubscriptionResponse_CancelSubRequestId",
                table: "BLTouchAndPlayCacelSubscriptionResponse",
                column: "CancelSubRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLTouchAndPlayCacelSubscriptionResponse");

            migrationBuilder.DropTable(
                name: "BLTouchAndPlayCacelSubscriptionRequest");
        }
    }
}
