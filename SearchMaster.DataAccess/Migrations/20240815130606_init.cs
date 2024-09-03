using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchMaster.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    About = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientToWorkerReviews",
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
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    TextData = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WriterId = table.Column<Guid>(type: "uuid", nullable: false),
                    HolderId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerToClientReviews_HolderId",
                table: "WorkerToClientReviews",
                column: "HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerToClientReviews_WriterId",
                table: "WorkerToClientReviews",
                column: "WriterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientToWorkerReviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WorkerToClientReviews");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
