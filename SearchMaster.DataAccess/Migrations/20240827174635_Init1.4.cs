using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("40ba0839-2d41-4f55-902b-1850405caeb6"));

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname" },
                values: new object[] { new Guid("bedc8788-b094-416b-9eb3-18c608ae93be"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("bedc8788-b094-416b-9eb3-18c608ae93be"));

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname" },
                values: new object[] { new Guid("40ba0839-2d41-4f55-902b-1850405caeb6"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов" });
        }
    }
}
