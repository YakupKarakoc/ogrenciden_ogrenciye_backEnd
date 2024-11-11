using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		public int SellerId { get; set; } // foreign key -> User
		public string Category { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal AiSuggestedPrice { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Status { get; set; }

		[ForeignKey("SellerId")]
		public User Seller { get; set; } // Navigation property for User
	}
}
