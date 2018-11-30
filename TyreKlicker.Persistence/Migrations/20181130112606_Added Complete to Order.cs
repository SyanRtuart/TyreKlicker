using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class AddedCompletetoOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "Order");
        }
    }
}
