using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class ProductRating
	{
		[Key]
		public int RatingId { get; set; }

		public int UserId { get; set; } // foreign key -> User
		public int ProductId { get; set; } // foreign key -> Product
		public decimal RatingValue { get; set; }
		public string Review { get; set; }
		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User

		[ForeignKey("ProductId")]
		public Product Product { get; set; } // Navigation property for Product
	}
}
