using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6c3c0385-38c1-4076-92b5-4c87d729a081"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname" },
                values: new object[] { new Guid("40ba0839-2d41-4f55-902b-1850405caeb6"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("40ba0839-2d41-4f55-902b-1850405caeb6"));

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Orders",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname" },
                values: new object[] { new Guid("6c3c0385-38c1-4076-92b5-4c87d729a081"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов" });
        }
    }
}
