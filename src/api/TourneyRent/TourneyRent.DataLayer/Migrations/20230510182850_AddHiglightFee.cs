using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddHiglightFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("5019d333-9a5d-4fbc-b691-4ac63b34ba24"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("8f2ccee9-34fb-422c-9b47-fb0eecb002dd"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("f062a606-ac3c-40c2-807e-d13ef2bd1218"));

            migrationBuilder.AddColumn<decimal>(
                name: "HighlightFee",
                table: "RentalItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("0c1f9892-ba53-47bc-9c7e-da643df4b491"), "Gaming monitor", null, "DELL Latitude 5490 (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("22cf3875-1eb7-4f17-a69a-0ad16540388f"), "Gaming monitor", null, "KOORUI 24.5 Inch FHD Gaming Monitor (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("4f2e53f3-377a-4db9-87d1-71b8a2dde5c6"), "Laptop", null, "Dell Latitude 3520 Laptop 15.6 (used)", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("0c1f9892-ba53-47bc-9c7e-da643df4b491"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("22cf3875-1eb7-4f17-a69a-0ad16540388f"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("4f2e53f3-377a-4db9-87d1-71b8a2dde5c6"));

            migrationBuilder.DropColumn(
                name: "HighlightFee",
                table: "RentalItems");

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("5019d333-9a5d-4fbc-b691-4ac63b34ba24"), "Gaming monitor", null, "KOORUI 24.5 Inch FHD Gaming Monitor (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("8f2ccee9-34fb-422c-9b47-fb0eecb002dd"), "Laptop", null, "Dell Latitude 3520 Laptop 15.6 (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("f062a606-ac3c-40c2-807e-d13ef2bd1218"), "Gaming monitor", null, "DELL Latitude 5490 (used)", null });
        }
    }
}
