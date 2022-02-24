using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class changebkashrecurringinitiatecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "redirectURL",
                table: "CreateSubscriptionInitiate_Bkash",
                newName: "redirectUrl");

            migrationBuilder.RenameColumn(
                name: "frequecy",
                table: "CreateSubscriptionInitiate_Bkash",
                newName: "frequency");

            migrationBuilder.AlterColumn<string>(
                name: "maxCapAmount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "firstPaymentAmount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "amount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "amountQueryUrl",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "maxCapRequired",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amountQueryUrl",
                table: "CreateSubscriptionInitiate_Bkash");

            migrationBuilder.DropColumn(
                name: "maxCapRequired",
                table: "CreateSubscriptionInitiate_Bkash");

            migrationBuilder.RenameColumn(
                name: "redirectUrl",
                table: "CreateSubscriptionInitiate_Bkash",
                newName: "redirectURL");

            migrationBuilder.RenameColumn(
                name: "frequency",
                table: "CreateSubscriptionInitiate_Bkash",
                newName: "frequecy");

            migrationBuilder.AlterColumn<decimal>(
                name: "maxCapAmount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "firstPaymentAmount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "CreateSubscriptionInitiate_Bkash",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
