using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class DeletePhotosEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Photos_PhotosId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PhotosId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PhotosId",
                table: "Profiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotosId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PhotosId",
                table: "Profiles",
                column: "PhotosId",
                unique: true,
                filter: "[PhotosId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Photos_PhotosId",
                table: "Profiles",
                column: "PhotosId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
