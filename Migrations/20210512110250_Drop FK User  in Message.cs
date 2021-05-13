using Microsoft.EntityFrameworkCore.Migrations;

namespace TalktifAPI.Migrations
{
    public partial class DropFKUserinMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Message__sender__7E37BEF6",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_sender",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Message_sender",
                table: "Message",
                column: "sender");

            migrationBuilder.AddForeignKey(
                name: "FK__Message__sender__7E37BEF6",
                table: "Message",
                column: "sender",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
