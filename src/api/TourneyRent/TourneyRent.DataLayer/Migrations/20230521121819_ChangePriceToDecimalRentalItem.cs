using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class ChangePriceToDecimalRentalItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RentalItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("0263fe8f-5c40-4087-9f9e-fe2ae039a693"), "Gaming monitor", null, "KOORUI 24.5 Inch FHD Gaming Monitor (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("6269492d-281d-4005-a88b-adec7ab442d5"), "Laptop", null, "Dell Latitude 3520 Laptop 15.6 (used)", null });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "Description", "ImageId", "Name", "TournamentId" },
                values: new object[] { new Guid("91e56181-459d-4cde-bb85-d267cdeba1d7"), "Gaming monitor", null, "DELL Latitude 5490 (used)", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("0263fe8f-5c40-4087-9f9e-fe2ae039a693"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("6269492d-281d-4005-a88b-adec7ab442d5"));

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: new Guid("91e56181-459d-4cde-bb85-d267cdeba1d7"));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "RentalItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
