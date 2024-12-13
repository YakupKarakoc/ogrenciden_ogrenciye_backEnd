using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class RoommateAd
	{
		[Key]
		public int RoommateAdId { get; set; }

		public int UserId { get; set; } // Foreign key -> User

		[Required]
		public string Location { get; set; }

		[Required]
		public decimal Rent { get; set; }

		[Required]
		public string Preferences { get; set; }

		public DateTime CreatedAt { get; set; }

		[Required]
		public string Status { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User
	}
}
