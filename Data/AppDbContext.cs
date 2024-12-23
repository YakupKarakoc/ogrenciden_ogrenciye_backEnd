using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ogrenciden_ogrenciye.Models;

namespace ogrenciden_ogrenciye.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<NoteFavorite> NoteFavorites { get; set; }
		public DbSet<ProductFavorite> ProductFavorites { get; set; }
		public DbSet<CourseAd> CourseAds { get; set; }
		public DbSet<RoommateAd> RoommateAds { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<UserSurvey> UserSurveys { get; set; }
		public DbSet<UserPreferences> UserPreferences { get; set; }
		public DbSet<RoommateAdFavorite> RoommateAdFavorites { get; set; }
		public DbSet<NoteRating> NoteRatings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// User -> Notes relationship
			modelBuilder.Entity<User>()
				.HasMany(u => u.Notes)
				.WithOne(n => n.Uploader)
				.HasForeignKey(n => n.UploaderId)
				.OnDelete(DeleteBehavior.Cascade);

			// User -> NoteRatings relationship
			modelBuilder.Entity<User>()
				.HasMany(u => u.NoteRatings)
				.WithOne(nr => nr.User)
				.HasForeignKey(nr => nr.UserId)
				.OnDelete(DeleteBehavior.NoAction); // Avoid cycle or multiple cascade paths

			// NoteRating -> Note relationship
			modelBuilder.Entity<NoteRating>()
				.HasOne(nr => nr.Note)
				.WithMany(n => n.NoteRatings)
				.HasForeignKey(nr => nr.NoteId)
				.OnDelete(DeleteBehavior.Cascade);

			// NoteFavorite -> Note relationship
			modelBuilder.Entity<NoteFavorite>()
				.HasOne(nf => nf.Note)
				.WithMany()
				.HasForeignKey(nf => nf.NoteId)
				.OnDelete(DeleteBehavior.Cascade);

			// NoteFavorite -> User relationship
			modelBuilder.Entity<NoteFavorite>()
				.HasOne(nf => nf.User)
				.WithMany()
				.HasForeignKey(nf => nf.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// RoommateAd -> User relationship
			modelBuilder.Entity<RoommateAd>()
				.HasOne(r => r.User)
				.WithMany()
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// RoommateAdFavorite -> User relationship
			modelBuilder.Entity<RoommateAdFavorite>()
				.HasOne(f => f.User)
				.WithMany()
				.HasForeignKey(f => f.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// RoommateAdFavorite -> RoommateAd relationship
			modelBuilder.Entity<RoommateAdFavorite>()
				.HasOne(f => f.RoommateAd)
				.WithMany()
				.HasForeignKey(f => f.RoommateAdId)
				.OnDelete(DeleteBehavior.Cascade);

			// ProductFavorite -> User relationship
			modelBuilder.Entity<ProductFavorite>()
				.HasOne(f => f.User)
				.WithMany()
				.HasForeignKey(f => f.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// ProductFavorite -> Product relationship
			modelBuilder.Entity<ProductFavorite>()
				.HasOne(f => f.Product)
				.WithMany()
				.HasForeignKey(f => f.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			// CourseAd -> Price precision
			modelBuilder.Entity<CourseAd>()
				.Property(c => c.Price)
				.HasPrecision(18, 2);

			// Product -> Price precision
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			// Product -> Default Image
			modelBuilder.Entity<Product>()
				.Property(p => p.ImagePath)
				.HasDefaultValue("/images/default.jpg");

			// Message -> Sender relationship
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany()
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.NoAction);

			// Message -> Receiver relationship
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany()
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.NoAction);

			// UserSurvey -> User relationship
			modelBuilder.Entity<UserSurvey>()
				.HasOne(us => us.User)
				.WithMany()
				.HasForeignKey(us => us.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			// Suppress PendingModelChangesWarning
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}
	}
}
