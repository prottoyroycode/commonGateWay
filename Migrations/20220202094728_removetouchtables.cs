using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class removetouchtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BLTouchAndPlayResponse");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BLTouchAndPlayResponse");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BLTouchAndPlayResponse");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BLTouchAndPlayResponse");

            migrationBuilder.DropColumn(
                name: "client_UserID",
                table: "BLTouchAndPlayResponse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BLTouchAndPlayResponse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BLTouchAndPlayResponse",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BLTouchAndPlayResponse",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BLTouchAndPlayResponse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_UserID",
                table: "BLTouchAndPlayResponse",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
