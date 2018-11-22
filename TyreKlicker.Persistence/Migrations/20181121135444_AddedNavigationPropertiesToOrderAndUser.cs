using Microsoft.EntityFrameworkCore.Migrations;

namespace TyreKlicker.Persistence.Migrations
{
    public partial class AddedNavigationPropertiesToOrderAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_AcceptedByUserId",
                table: "Order",
                column: "AcceptedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreatedByUserId",
                table: "Order",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_AcceptedByUserId",
                table: "Order",
                column: "AcceptedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_CreatedByUserId",
                table: "Order",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_AcceptedByUserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_CreatedByUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AcceptedByUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CreatedByUserId",
                table: "Order");
        }
    }
}
