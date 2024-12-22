using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class RoommateAdFavorite
	{
		[Key]
		public int FavoriteId { get; set; }

		[Required]
		public int UserId { get; set; } // Kullanıcı ID'si (Foreign Key)

		[Required]
		public int RoommateAdId { get; set; } // İlgili RoommateAd ID'si (Foreign Key)

		public DateTime AddedDate { get; set; } = DateTime.UtcNow;

		[ForeignKey("UserId")]
		public User User { get; set; } // Kullanıcı ile ilişki

		[ForeignKey("RoommateAdId")]
		public RoommateAd RoommateAd { get; set; } // RoommateAd ile ilişki
	}
}
