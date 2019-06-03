using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class AddedStartFinishToAvailabilty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Availability",
                newName: "Start");

            migrationBuilder.AddColumn<DateTime>(
                name: "Finish",
                table: "Availability",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finish",
                table: "Availability");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Availability",
                newName: "DateTime");
        }
    }
}
