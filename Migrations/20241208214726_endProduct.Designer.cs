﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ogrenciden_ogrenciye.Models;

#nullable disable

namespace ogrenciden_ogrenciye.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241208214726_endProduct")]
    partial class endProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.CourseAd", b =>
                {
                    b.Property<int>("CourseAdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseAdId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CourseAdId");

                    b.HasIndex("SellerId");

                    b.ToTable("CourseAds");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteId"));

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrendStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UploaderId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("UploaderId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.NoteRating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.Property<decimal>("RatingValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("NoteId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("NoteRatings");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("/images/default.jpg");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.ProductRating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("RatingValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductRatings");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.RoommateAd", b =>
                {
                    b.Property<int>("RoommateAdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoommateAdId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Preferences")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoommateAdId");

                    b.HasIndex("UserId");

                    b.ToTable("RoommateAds");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.CourseAd", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Favorite", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ogrenciden_ogrenciye.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Note", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.User", "Uploader")
                        .WithMany("Notes")
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.NoteRating", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.Note", "Note")
                        .WithMany("NoteRatings")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ogrenciden_ogrenciye.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ogrenciden_ogrenciye.Models.User", null)
                        .WithMany("NoteRatings")
                        .HasForeignKey("UserId1");

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Product", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.ProductRating", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ogrenciden_ogrenciye.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.RoommateAd", b =>
                {
                    b.HasOne("ogrenciden_ogrenciye.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.Note", b =>
                {
                    b.Navigation("NoteRatings");
                });

            modelBuilder.Entity("ogrenciden_ogrenciye.Models.User", b =>
                {
                    b.Navigation("NoteRatings");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
