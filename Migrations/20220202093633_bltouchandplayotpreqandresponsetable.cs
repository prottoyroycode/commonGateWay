using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class bltouchandplayotpreqandresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayOtpRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Msisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionGroupID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayOtpRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLTouchAndPlayResponse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OTPString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BLTouchAndPlayOtpRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLTouchAndPlayResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BLTouchAndPlayResponse_BLTouchAndPlayOtpRequest_BLTouchAndPlayOtpRequestId",
                        column: x => x.BLTouchAndPlayOtpRequestId,
                        principalTable: "BLTouchAndPlayOtpRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLTouchAndPlayResponse_BLTouchAndPlayOtpRequestId",
                table: "BLTouchAndPlayResponse",
                column: "BLTouchAndPlayOtpRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLTouchAndPlayResponse");

            migrationBuilder.DropTable(
                name: "BLTouchAndPlayOtpRequest");
        }
    }
}
