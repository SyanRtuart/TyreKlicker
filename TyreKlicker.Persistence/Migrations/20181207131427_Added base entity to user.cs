using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class Addedbaseentitytouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "User",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "User",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "User");
        }
    }
}
