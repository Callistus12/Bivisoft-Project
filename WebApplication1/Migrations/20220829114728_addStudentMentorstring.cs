using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addStudentMentorstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CourseMentorship",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(String),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CourseMentorship",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
