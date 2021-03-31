using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class blabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "UserToken",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_at",
                table: "UserToken",
                type: "datetime2",
                maxLength: 200,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "addedAt",
                table: "User_Favs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "nickname",
                table: "User_Favs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "create_at",
                table: "UserToken");

            migrationBuilder.DropColumn(
                name: "addedAt",
                table: "User_Favs");

            migrationBuilder.DropColumn(
                name: "nickname",
                table: "User_Favs");
        }
    }
}
