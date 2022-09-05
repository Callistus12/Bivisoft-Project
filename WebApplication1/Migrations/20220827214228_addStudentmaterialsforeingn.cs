using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addStudentmaterialsforeingn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student materials_AspNetUsers_UserName",
                table: "student materials");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "student materials",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_student materials_UserName",
                table: "student materials",
                newName: "IX_student materials_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_student materials_AspNetUsers_UserId",
                table: "student materials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student materials_AspNetUsers_UserId",
                table: "student materials");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "student materials",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_student materials_UserId",
                table: "student materials",
                newName: "IX_student materials_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_student materials_AspNetUsers_UserName",
                table: "student materials",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
