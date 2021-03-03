using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class Talktif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isAdmin = table.Column<int>(type: "int", nullable: false),
                    creatAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MessagesItems",
                columns: table => new
                {
                    mesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    senderId = table.Column<int>(type: "int", nullable: false),
                    receiverId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    contentCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    creatAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesItems", x => x.mesId);
                    table.ForeignKey(
                        name: "FK_MessagesItems_UserItems_receiverId",
                        column: x => x.receiverId,
                        principalTable: "UserItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagesItems_UserItems_senderId",
                        column: x => x.senderId,
                        principalTable: "UserItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportItems",
                columns: table => new
                {
                    rpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rpterId = table.Column<int>(type: "int", nullable: false),
                    rpCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    rpDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    rpStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    creatAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportItems", x => x.rpId);
                    table.ForeignKey(
                        name: "FK_ReportItems_UserItems_rpterId",
                        column: x => x.rpterId,
                        principalTable: "UserItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessagesItems_receiverId",
                table: "MessagesItems",
                column: "receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesItems_senderId",
                table: "MessagesItems",
                column: "senderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportItems_rpterId",
                table: "ReportItems",
                column: "rpterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesItems");

            migrationBuilder.DropTable(
                name: "ReportItems");

            migrationBuilder.DropTable(
                name: "UserItems");
        }
    }
}
