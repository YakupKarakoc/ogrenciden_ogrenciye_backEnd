using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Rating
	{
		[Key]
		public int RatingId { get; set; }

		[Required]
		public int UserId { get; set; } // Foreign key -> User

		[Required]
		public int TargetId { get; set; } // ID of the rated entity (e.g., Note, Product)
		[Required]
		public string TargetType { get; set; } // Type of the rated entity (e.g., "Note", "Product")

		[Required]
		public decimal RatingValue { get; set; }
		public string Review { get; set; }
		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User
	}
}
