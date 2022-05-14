using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.DAL.Migrations
{
    public partial class PhotosUpdate1_la_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Profiles_UserProfileId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "PhotosId",
                table: "Profiles",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Photos_PhotosId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PhotosId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PhotosId",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Profiles_UserProfileId",
                table: "Photos",
                column: "UserProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
