using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Infrastructure.Identity.Migrations
{
    public partial class AddedTyreKlickerUserIdtoAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TyreKlickerUserId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TyreKlickerUserId",
                table: "AspNetUsers");
        }
    }
}
