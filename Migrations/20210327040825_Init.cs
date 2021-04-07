using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from = table.Column<int>(type: "int", nullable: false),
                    to = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    sentAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.id);
                    table.ForeignKey(
                        name: "FK__Message__from__2D27B809",
                        column: x => x.from,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reporter = table.Column<int>(type: "int", nullable: false),
                    suspect = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.id);
                    table.ForeignKey(
                        name: "FK__Report__reporter__30F848ED",
                        column: x => x.reporter,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Favs",
                columns: table => new
                {
                    user = table.Column<int>(type: "int", nullable: false),
                    favourite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Fav__A323CB9348DF7B6E", x => new { x.user, x.favourite });
                    table.ForeignKey(
                        name: "FK__User_Favs__user__29572725",
                        column: x => x.user,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uid = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    token = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Token__A323CB9448DF7B6E", x => x.id);
                    table.ForeignKey(
                        name: "FK__User_Token__user__29572725",
                        column: x => x.uid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_from",
                table: "Message",
                column: "from");

            migrationBuilder.CreateIndex(
                name: "IX_Report_reporter",
                table: "Report",
                column: "reporter");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164410B6ED7",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_uid",
                table: "UserToken",
                column: "uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "User_Favs");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
