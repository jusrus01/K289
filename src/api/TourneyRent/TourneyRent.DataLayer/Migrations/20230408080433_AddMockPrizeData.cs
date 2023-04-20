using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddMockPrizeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Tournaments_TournamentId",
                table: "Prize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prize",
                table: "Prize");

            migrationBuilder.RenameTable(
                name: "Prize",
                newName: "Prizes");

            migrationBuilder.RenameIndex(
                name: "IX_Prize_TournamentId",
                table: "Prizes",
                newName: "IX_Prizes_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prizes",
                table: "Prizes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("16157e63-9308-4a3b-8493-f8b030886da5"), "Gaming monitor", null, "KOORUI 24.5 Inch FHD Gaming Monitor (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("28d718d3-440f-4110-8af6-a5fa5cdbeb17"), "Laptop", null, "Dell Latitude 3520 Laptop 15.6 (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("a4642138-fdd9-41db-b408-80cb5a3f7c5b"), "Gaming monitor", null, "DELL Latitude 5490 (used)", null });

            migrationBuilder.AddForeignKey(
                name: "FK_Prizes_Tournaments_TournamentId",
                table: "Prizes",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prizes_Tournaments_TournamentId",
                table: "Prizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prizes",
                table: "Prizes");

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("16157e63-9308-4a3b-8493-f8b030886da5"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("28d718d3-440f-4110-8af6-a5fa5cdbeb17"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("a4642138-fdd9-41db-b408-80cb5a3f7c5b"));

            migrationBuilder.RenameTable(
                name: "Prizes",
                newName: "Prize");

            migrationBuilder.RenameIndex(
                name: "IX_Prizes_TournamentId",
                table: "Prize",
                newName: "IX_Prize_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prize",
                table: "Prize",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Tournaments_TournamentId",
                table: "Prize",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }
    }
}
