using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ogrenciden_ogrenciye.Migrations
{
    /// <inheritdoc />
    public partial class addSellerIdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
	name: "SellerId",
	table: "Products",
	nullable: false,
	defaultValue: 0);

			migrationBuilder.AddForeignKey(
				name: "FK_Products_Users_SellerId",
				table: "Products",
				column: "SellerId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
	name: "FK_Products_Users_SellerId",
	table: "Products");

			migrationBuilder.DropColumn(
				name: "SellerId",
				table: "Products");

		}
	}
}
