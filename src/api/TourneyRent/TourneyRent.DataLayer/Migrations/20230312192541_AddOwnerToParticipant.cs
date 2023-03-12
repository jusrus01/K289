using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddOwnerToParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Tournaments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OwnerId",
                table: "Tournaments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_OwnerId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tournaments");
        }
    }
}
