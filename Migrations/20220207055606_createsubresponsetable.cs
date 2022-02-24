using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class createsubresponsetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreateSubscriptionResponse_Bkash",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscriptionRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    redirectURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expirationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    errorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    errorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_UserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateSubscriptionResponse_Bkash", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateSubscriptionResponse_Bkash");
        }
    }
}
