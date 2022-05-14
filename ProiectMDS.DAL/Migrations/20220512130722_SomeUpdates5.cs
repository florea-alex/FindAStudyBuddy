using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class SomeUpdates5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseCourses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    credits = table.Column<int>(type: "int", nullable: false),
                    teacherName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCourses", x => x.CourseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseCourses");
        }
    }
}
