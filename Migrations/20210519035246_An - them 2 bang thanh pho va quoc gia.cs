using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class Anthem2bangthanhphovaquocgia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Message__chatRoo__7F2BE32F",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK__Report__reporter__73BA3083",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Chat__chatR__7B5B524B",
                table: "User_ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK__User_ChatR__user__7A672E12",
                table: "User_ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Refre__user__628FA481",
                table: "User_RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK__User_Cha__4372E63A933B1139",
                table: "User_ChatRoom");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "User_ChatRoom",
                newName: "nickName");

            migrationBuilder.RenameIndex(
                name: "UQ__User__AB6E616459FFB8D3",
                table: "User",
                newName: "UQ__User__AB6E6164565CBAC3");

            migrationBuilder.AlterColumn<string>(
                name: "device",
                table: "User_RefreshToken",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "nickName",
                table: "User_ChatRoom",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "confirmedEmail",
                table: "User",
                type: "bit",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cityId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK__User_Cha__4372E63ACB17237C",
                table: "User_ChatRoom",
                columns: new[] { "user", "chatRoomId" });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.id);
                    table.ForeignKey(
                        name: "FK__City__countryId__7849DB76",
                        column: x => x.countryId,
                        principalTable: "Country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_cityId",
                table: "User",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_countryId",
                table: "City",
                column: "countryId");

            migrationBuilder.AddForeignKey(
                name: "FK__Message__chatRoo__2334397B",
                table: "Message",
                column: "chatRoomId",
                principalTable: "Chat_Room",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Report__reporter__2610A626",
                table: "Report",
                column: "reporter",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User__cityId__14E61A24",
                table: "User",
                column: "cityId",
                principalTable: "City",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_Chat__chatR__2057CCD0",
                table: "User_ChatRoom",
                column: "chatRoomId",
                principalTable: "Chat_Room",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_ChatR__user__1F63A897",
                table: "User_ChatRoom",
                column: "user",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_Refre__user__1A9EF37A",
                table: "User_RefreshToken",
                column: "user",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Message__chatRoo__2334397B",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK__Report__reporter__2610A626",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK__User__cityId__14E61A24",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Chat__chatR__2057CCD0",
                table: "User_ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK__User_ChatR__user__1F63A897",
                table: "User_ChatRoom");

            migrationBuilder.DropForeignKey(
                name: "FK__User_Refre__user__1A9EF37A",
                table: "User_RefreshToken");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK__User_Cha__4372E63ACB17237C",
                table: "User_ChatRoom");

            migrationBuilder.DropIndex(
                name: "IX_User_cityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "nickName",
                table: "User_ChatRoom",
                newName: "nickname");

            migrationBuilder.RenameIndex(
                name: "UQ__User__AB6E6164565CBAC3",
                table: "User",
                newName: "UQ__User__AB6E616459FFB8D3");

            migrationBuilder.AlterColumn<string>(
                name: "device",
                table: "User_RefreshToken",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "nickname",
                table: "User_ChatRoom",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<bool>(
                name: "confirmedEmail",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddPrimaryKey(
                name: "PK__User_Cha__4372E63A933B1139",
                table: "User_ChatRoom",
                columns: new[] { "user", "chatRoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK__Message__chatRoo__7F2BE32F",
                table: "Message",
                column: "chatRoomId",
                principalTable: "Chat_Room",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Report__reporter__73BA3083",
                table: "Report",
                column: "reporter",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_Chat__chatR__7B5B524B",
                table: "User_ChatRoom",
                column: "chatRoomId",
                principalTable: "Chat_Room",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_ChatR__user__7A672E12",
                table: "User_ChatRoom",
                column: "user",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__User_Refre__user__628FA481",
                table: "User_RefreshToken",
                column: "user",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
