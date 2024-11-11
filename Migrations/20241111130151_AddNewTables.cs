﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ogrenciden_ogrenciye.Migrations
{
	public partial class AddNewTables : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Users tablosunu yeniden oluşturmaktan kaçınıyoruz

			migrationBuilder.CreateTable(
				name: "CourseAds",
				columns: table => new
				{
					AdId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CourseAds", x => x.AdId);
					table.ForeignKey(
						name: "FK_CourseAds_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Favorites",
				columns: table => new
				{
					FavoriteId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					ItemId = table.Column<int>(type: "int", nullable: false),
					ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
					AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Favorites", x => x.FavoriteId);
					table.ForeignKey(
						name: "FK_Favorites_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Notes",
				columns: table => new
				{
					NoteId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UploaderId = table.Column<int>(type: "int", nullable: false),
					Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					TrendStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Notes", x => x.NoteId);
					table.ForeignKey(
						name: "FK_Notes_Users_UploaderId",
						column: x => x.UploaderId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					ProductId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SellerId = table.Column<int>(type: "int", nullable: false),
					Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					AiSuggestedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.ProductId);
					table.ForeignKey(
						name: "FK_Products_Users_SellerId",
						column: x => x.SellerId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "RoommateAds",
				columns: table => new
				{
					RoommateAdId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Preferences = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RoommateAds", x => x.RoommateAdId);
					table.ForeignKey(
						name: "FK_RoommateAds_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "NoteRatings",
				columns: table => new
				{
					RatingId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					NoteId = table.Column<int>(type: "int", nullable: false),
					RatingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Date = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NoteRatings", x => x.RatingId);
					table.ForeignKey(
						name: "FK_NoteRatings_Notes_NoteId",
						column: x => x.NoteId,
						principalTable: "Notes",
						principalColumn: "NoteId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_NoteRatings_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ProductRatings",
				columns: table => new
				{
					RatingId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					ProductId = table.Column<int>(type: "int", nullable: false),
					RatingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Date = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductRatings", x => x.RatingId);
					table.ForeignKey(
						name: "FK_ProductRatings_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "ProductId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ProductRatings_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_CourseAds_UserId",
				table: "CourseAds",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Favorites_UserId",
				table: "Favorites",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_NoteRatings_NoteId",
				table: "NoteRatings",
				column: "NoteId");

			migrationBuilder.CreateIndex(
				name: "IX_NoteRatings_UserId",
				table: "NoteRatings",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Notes_UploaderId",
				table: "Notes",
				column: "UploaderId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductRatings_ProductId",
				table: "ProductRatings",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductRatings_UserId",
				table: "ProductRatings",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_SellerId",
				table: "Products",
				column: "SellerId");

			migrationBuilder.CreateIndex(
				name: "IX_RoommateAds_UserId",
				table: "RoommateAds",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CourseAds");

			migrationBuilder.DropTable(
				name: "Favorites");

			migrationBuilder.DropTable(
				name: "NoteRatings");

			migrationBuilder.DropTable(
				name: "ProductRatings");

			migrationBuilder.DropTable(
				name: "RoommateAds");

			migrationBuilder.DropTable(
				name: "Notes");

			migrationBuilder.DropTable(
				name: "Products");

			// Users tablosunu silmeyin
		}
	}
}
