using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddOwnerToRentalItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "RentalItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RentalItems_OwnerId",
                table: "RentalItems",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems");

            migrationBuilder.DropIndex(
                name: "IX_RentalItems_OwnerId",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "RentalItems");
        }
    }
}
