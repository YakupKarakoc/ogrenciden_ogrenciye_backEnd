using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ogrenciden_ogrenciye.Migrations
{
    /// <inheritdoc />
    public partial class addProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteRatings_Notes_NoteId",
                table: "NoteRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteRatings_Notes_NoteId1",
                table: "NoteRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Users_UserId",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_NoteRatings_NoteId1",
                table: "NoteRatings");

            migrationBuilder.DropColumn(
                name: "NoteId1",
                table: "NoteRatings");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "/images/default.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRatings_Notes_NoteId",
                table: "NoteRatings",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Users_UserId",
                table: "ProductRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteRatings_Notes_NoteId",
                table: "NoteRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Users_UserId",
                table: "ProductRatings");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "/images/default.jpg");

            migrationBuilder.AddColumn<int>(
                name: "NoteId1",
                table: "NoteRatings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteRatings_NoteId1",
                table: "NoteRatings",
                column: "NoteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRatings_Notes_NoteId",
                table: "NoteRatings",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRatings_Notes_NoteId1",
                table: "NoteRatings",
                column: "NoteId1",
                principalTable: "Notes",
                principalColumn: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Users_UserId",
                table: "ProductRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
