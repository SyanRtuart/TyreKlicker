using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class ChangedOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "CreatedByUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "AcceptedByUserId",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedByUserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Order",
                newName: "UserId");
        }
    }
}