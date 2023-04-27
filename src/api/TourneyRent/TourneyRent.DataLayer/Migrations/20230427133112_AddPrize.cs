using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddPrize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReason",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tournaments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountName",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamMembers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "RentalItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Participants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "CalendarItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prize_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prize_TournamentId",
                table: "Prizes",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_AspNetUsers_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Tournaments_TournamentId",
                table: "Prizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prize",
                table: "Prizes");

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
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_AspNetUsers_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionReason",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tournaments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountName",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamMembers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "RentalItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "RentalItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Participants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "CalendarItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarItems_AspNetUsers_BuyerId",
                table: "CalendarItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_AspNetUsers_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_AspNetUsers_OwnerId",
                table: "RentalItems",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_AspNetUsers_OwnerId",
                table: "Tournaments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.RenameIndex(
                name: "IX_Prizes_TournamentId",
                table: "Prizes",
                newName: "IX_Prize_TournamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prize",
                table: "Prizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Tournaments_TournamentId",
                table: "Prizes",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id");

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
