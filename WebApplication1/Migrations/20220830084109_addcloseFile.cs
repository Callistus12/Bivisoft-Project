using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addcloseFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CloseFile",
                table: "student materials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<String>(
                name: "Id",
                table: "CourseMentorship",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseFile",
                table: "student materials");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CourseMentorship",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
