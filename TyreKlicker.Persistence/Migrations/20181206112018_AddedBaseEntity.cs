using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class AddedBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "OrderId");
        }
    }
}
