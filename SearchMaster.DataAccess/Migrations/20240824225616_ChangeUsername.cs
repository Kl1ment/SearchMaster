using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Persons_Username",
                table: "Persons");

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("370887e2-1c12-400c-a87a-739998b451d9"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname" },
                values: new object[] { new Guid("6c3c0385-38c1-4076-92b5-4c87d729a081"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("6c3c0385-38c1-4076-92b5-4c87d729a081"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Persons_Username",
                table: "Persons",
                column: "Username");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Rating", "RoleId", "Surname", "Username" },
                values: new object[] { new Guid("370887e2-1c12-400c-a87a-739998b451d9"), "PersonEntity", "admin@gmail.com", "Климент", 0f, 1, "Иванов", "Admin" });
        }
    }
}
