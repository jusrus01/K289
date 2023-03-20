using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddCalendarEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CalendarItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarItems_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarItems_RentalItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "RentalItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TournamentId",
                table: "Participants",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarItems_BuyerId",
                table: "CalendarItems",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarItems_ItemId",
                table: "CalendarItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Tournaments_TournamentId",
                table: "Participants",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Tournaments_TournamentId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "CalendarItems");

            migrationBuilder.DropIndex(
                name: "IX_Participants_TournamentId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Participants");
        }
    }
}
