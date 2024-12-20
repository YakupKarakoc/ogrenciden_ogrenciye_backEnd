using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Models;

namespace ogrenciden_ogrenciye.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Favorite> Favorites { get; set; }
		public DbSet<CourseAd> CourseAds { get; set; }
		public DbSet<RoommateAd> RoommateAds { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<UserSurvey> UserSurveys { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Note -> NoteRating ilişkisi
			modelBuilder.Entity<NoteRating>()
				.HasOne(nr => nr.Note)
				.WithMany(n => n.NoteRatings)
				.HasForeignKey(nr => nr.NoteId)
				.OnDelete(DeleteBehavior.Cascade);

			// Note -> User ilişkisi
			modelBuilder.Entity<Note>()
				.HasOne(n => n.Uploader)
				.WithMany()
				.HasForeignKey(n => n.UploaderId)
				.OnDelete(DeleteBehavior.NoAction);

			// Product ilişkisi: Ürün ve satıcı ilişkisi
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Seller)
				.WithMany()
				.HasForeignKey(p => p.SellerId)
				.OnDelete(DeleteBehavior.NoAction);

			// Product varsayılan görsel
			modelBuilder.Entity<Product>()
				.Property(p => p.ImagePath)
				.HasDefaultValue("/images/default.jpg");

			// Decimal hassasiyet (Price, RatingValue vb.)
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			modelBuilder.Entity<CourseAd>()
				.Property(c => c.Price)
				.HasPrecision(18, 2);

			modelBuilder.Entity<NoteRating>()
				.Property(nr => nr.RatingValue)
				.HasPrecision(18, 2);

			// Message ilişkisi: Gönderen ve alıcı arasında bağlantı
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany()
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany()
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.NoAction);

			// RoommateAd -> User ilişkisi
			modelBuilder.Entity<RoommateAd>()
				.HasOne(r => r.User)
				.WithMany()
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// UserSurvey -> User ilişkisi
			modelBuilder.Entity<UserSurvey>()
				.HasOne(us => us.User)
				.WithMany()
				.HasForeignKey(us => us.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			// PendingModelChangesWarning uyarısını bastırmak için (isteğe bağlı)
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
		}
	}
}
