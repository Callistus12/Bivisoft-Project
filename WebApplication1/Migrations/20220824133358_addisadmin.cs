using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calischool.Migrations
{
    public partial class addisadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            
        }
    }
}
