using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class CourseAd
	{
		[Key]
		public int CourseAdId { get; set; }

		[Required]
		public int SellerId { get; set; } // Foreign key -> User

		[Required]
		[MaxLength(100)]
		public string Title { get; set; } // Ad title

		[Required]
		public string Description { get; set; } // Ad description

		[Required]
		public decimal Price { get; set; } // Price for the course

		[Required]
		public string Category { get; set; } // Course category

		public string Location { get; set; } // Optional location

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[ForeignKey("SellerId")]
		public User Seller { get; set; } // Navigation property for User
	}
}
