using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class CourseAd
	{
		[Key]
		public int AdId { get; set; }

		public int UserId { get; set; } // foreign key -> User
		public string Subject { get; set; }
		public string Description { get; set; }
		public decimal PricePerHour { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Status { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User
	}
}
