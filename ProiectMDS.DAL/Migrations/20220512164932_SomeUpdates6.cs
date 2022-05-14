using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class SomeUpdates6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "teacherName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "teacherName",
                table: "BaseCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "teacherName",
                table: "Courses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "teacherName",
                table: "BaseCourses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
