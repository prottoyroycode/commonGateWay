using Microsoft.EntityFrameworkCore.Migrations;

namespace Bkash_Service_API.Migrations
{
    public partial class create_BL_GakkService_detailNdefination_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BLChargeGakkServiceDefinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeBtdVatSd = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLChargeGakkServiceDefinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLChargeGakkServiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceDefinationId = table.Column<int>(type: "int", nullable: false),
                    GakkServiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpPass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    interval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLChargeGakkServiceDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLChargeGakkServiceDefinations");

            migrationBuilder.DropTable(
                name: "BLChargeGakkServiceDetails");
        }
    }
}
