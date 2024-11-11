using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Rating
	{
		public int RatingId { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; } // foreign key -> User

		public int TargetId { get; set; } // Target entity id (e.g., Note, Product, etc.)
		public string TargetType { get; set; } // Entity type (e.g., "Note", "Product")

		public decimal RatingValue { get; set; }
		public string Review { get; set; }
		public DateTime Date { get; set; }

		public User User { get; set; } // Navigation property for User
	}
}
