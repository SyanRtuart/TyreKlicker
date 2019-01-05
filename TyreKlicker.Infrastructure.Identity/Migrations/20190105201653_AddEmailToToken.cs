using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Infrastructure.Identity.Migrations
{
    public partial class AddEmailToToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "RefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "RefreshTokens");
        }
    }
}