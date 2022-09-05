using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class adddeleuuuu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
               name: "Deactiveted",
               table: "AspNetUsers",
               type: "bit",
               nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "Deactiveted",
             table: "AspNetUsers");
        }
    }
}
