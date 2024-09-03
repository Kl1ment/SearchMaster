using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ClientToWorkerReviews");

            migrationBuilder.DropTable(
                name: "WorkerToClientReviews");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "Profession",
                table: "Persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Persons",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Persons",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    TextData = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Persons_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Persons_WriterId",
                        column: x => x.WriterId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HolderId",
                table: "Reviews",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WriterId",
                table: "Reviews",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Persons_ClientId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Workers");

            migrationBuilder.AlterColumn<string>(
                name: "Profession",
                table: "Workers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Workers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientToWorkerReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    TextData = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientToWorkerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientToWorkerReviews_Clients_WriterId",
                        column: x => x.WriterId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientToWorkerReviews_Workers_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerToClientReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderId = table.Column<Guid>(type: "uuid", nullable: false),
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    TextData = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerToClientReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerToClientReviews_Clients_HolderId",
                        column: x => x.HolderId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerToClientReviews_Workers_WriterId",
                        column: x => x.WriterId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientToWorkerReviews_HolderId",
                table: "ClientToWorkerReviews",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientToWorkerReviews_WriterId",
                table: "ClientToWorkerReviews",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerToClientReviews_HolderId",
                table: "WorkerToClientReviews",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerToClientReviews_WriterId",
                table: "WorkerToClientReviews",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
