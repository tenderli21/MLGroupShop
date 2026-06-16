using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLGroupShop.Migrations
{
    public partial class AddCustomFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "CustomOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CustomOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "CustomOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomOrders");
        }
    }
}
