using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class RoommateAd
	{
		[Key]
		public int AdId { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[MaxLength(50)]
		public string Location { get; set; }

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal Budget { get; set; }

		[MaxLength(10)]
		public string GenderPreference { get; set; }

		public string Features { get; set; } // Özellikler (örn. "Sessiz, Eşyalı")

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[Required]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
