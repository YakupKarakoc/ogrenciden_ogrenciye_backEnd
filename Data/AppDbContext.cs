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
	}
}
