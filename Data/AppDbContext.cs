using Microsoft.EntityFrameworkCore;

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
		public DbSet<NoteRating> NoteRatings { get; set; }
		public DbSet<ProductRating> ProductRatings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Kullanıcıların not puanlamaları (NoteRating) ilişkisi
			modelBuilder.Entity<NoteRating>()
				.HasOne(nr => nr.User)
				.WithMany()
				.HasForeignKey(nr => nr.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// Ürünlerin puanlamaları (ProductRating) ilişkisi
			modelBuilder.Entity<ProductRating>()
				.HasOne(pr => pr.Product)
				.WithMany()
				.HasForeignKey(pr => pr.ProductId)
				.OnDelete(DeleteBehavior.NoAction);

			// Ürünlerin kullanıcılarla ilişkisi (örnek)
			modelBuilder.Entity<Product>()
				.Property(p => p.ImagePath)
				.HasDefaultValue("/images/default.jpg"); // Varsayılan görsel yolu

			base.OnModelCreating(modelBuilder);


		}
	}
}
