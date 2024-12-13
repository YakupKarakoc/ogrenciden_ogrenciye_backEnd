using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }

		public string ImagePath { get; set; } = "/images/default.jpg";

		[Required]
		public string Category { get; set; } // Kategori

		
		public int SellerId { get; set; } // Foreign key

		[ForeignKey("SellerId")]
		public User Seller { get; set; } // Navigation property

		[Required]
		[EmailAddress]
		public string SellerEmail { get; set; } // Satıcı e-posta

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
