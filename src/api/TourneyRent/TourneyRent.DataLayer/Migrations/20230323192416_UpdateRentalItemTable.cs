using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class UpdateRentalItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "RentalItems",
                newName: "TransactionReason");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "RentalItems",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "RentalItems");

            migrationBuilder.RenameColumn(
                name: "TransactionReason",
                table: "RentalItems",
                newName: "Image");
        }
    }
}
