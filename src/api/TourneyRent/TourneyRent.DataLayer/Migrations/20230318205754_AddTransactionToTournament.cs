using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddTransactionToTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Tournaments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TransactionId",
                table: "Tournaments",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Transactions_TransactionId",
                table: "Tournaments",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Transactions_TransactionId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_TransactionId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Tournaments");
        }
    }
}
