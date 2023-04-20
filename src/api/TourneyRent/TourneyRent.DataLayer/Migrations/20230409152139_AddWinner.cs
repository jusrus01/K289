using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsWinnerSelected",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "Participants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("239a3846-3852-4e89-99e9-f4d7f066460d"), "Gaming monitor", null, "KOORUI 24.5 Inch FHD Gaming Monitor (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("57cb55f2-ec7a-4a47-862e-f08da07fccd7"), "Gaming monitor", null, "DELL Latitude 5490 (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("f192bf13-861f-4e50-a406-5ffcbeb7278a"), "Laptop", null, "Dell Latitude 3520 Laptop 15.6 (used)", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("239a3846-3852-4e89-99e9-f4d7f066460d"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("57cb55f2-ec7a-4a47-862e-f08da07fccd7"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("f192bf13-861f-4e50-a406-5ffcbeb7278a"));

            migrationBuilder.DropColumn(
                name: "IsWinnerSelected",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "Participants");

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
        }
    }
}
