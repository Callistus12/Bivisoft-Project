using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addmaterialsFromUserPdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "student materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student materials_AspNetUsers_UserName",
                        column: x => x.UserName,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_student materials_UserName",
                table: "student materials",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student materials");
        }
    }
}
