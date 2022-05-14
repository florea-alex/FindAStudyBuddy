using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class PhotosUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");
        }
    }
}
