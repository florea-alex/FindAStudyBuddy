using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class PhotosUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
