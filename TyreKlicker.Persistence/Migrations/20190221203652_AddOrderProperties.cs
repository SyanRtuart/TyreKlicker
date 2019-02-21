using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class AddOrderProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trim",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tyre",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Make",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Trim",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Tyre",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Order");
        }
    }
}
