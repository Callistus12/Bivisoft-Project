using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addStudentMentorIsAssign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "CourseMentorship",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "CourseMentorship");
        }
    }
}
