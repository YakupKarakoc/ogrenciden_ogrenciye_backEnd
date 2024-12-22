using ogrenciden_ogrenciye.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class ProductFavorite
{
	[Key]
	public int FavoriteId { get; set; }

	[Required]
	public int UserId { get; set; } // Foreign Key -> User

	[Required]
	public int ProductId { get; set; } // Foreign Key -> Product

	public DateTime AddedDate { get; set; } = DateTime.UtcNow;

	[ForeignKey("UserId")]
	public User User { get; set; }

	[ForeignKey("ProductId")]
	public Product Product { get; set; }
}
