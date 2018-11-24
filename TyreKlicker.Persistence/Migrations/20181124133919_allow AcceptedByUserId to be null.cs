using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class allowAcceptedByUserIdtobenull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AcceptedByUserId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AcceptedByUserId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
