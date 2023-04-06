using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class CalendarItemChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "CalendarItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "CalendarItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
