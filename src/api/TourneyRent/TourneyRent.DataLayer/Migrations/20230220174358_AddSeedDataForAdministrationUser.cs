using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourneyRent.DataLayer.Migrations
{
    public partial class AddSeedDataForAdministrationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "513ee5ae-8d69-4e38-95d4-cacbb4cc24c7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bca7c0cf-de3b-441a-9a93-29667a913469", "3d887dc2-f10e-4b6a-be3b-4e36870d73f8", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2f2c8f88-466a-46a7-973e-2e846effdf24", 0, "d9613afe-bf71-4011-ab09-10540da12750", "admin@admin.com", false, "Vardenis", "Pavardenis", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAENlhkWeGcBe8J95nTzkIaLj9eD0pR7vaJ4+89LD2P0HMrraG2ZvpWiBn1+hpn9NA3A==", null, false, "37QVYJOLLDOSH5MWJSLBOWGDCAXPT2QA", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bca7c0cf-de3b-441a-9a93-29667a913469", "2f2c8f88-466a-46a7-973e-2e846effdf24" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bca7c0cf-de3b-441a-9a93-29667a913469", "2f2c8f88-466a-46a7-973e-2e846effdf24" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bca7c0cf-de3b-441a-9a93-29667a913469");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2f2c8f88-466a-46a7-973e-2e846effdf24");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "513ee5ae-8d69-4e38-95d4-cacbb4cc24c7", "6b017b9e-f1f0-490d-87aa-233fbf864f1d", "Administrator", "ADMINISTRATOR" });
        }
    }
}
