using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class SomeUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "BaseCourses",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BaseCourses",
                newName: "courseId");
        }
    }
}
