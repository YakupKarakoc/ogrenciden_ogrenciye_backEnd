using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Favorite
	{
		[Key]
		public int FavoriteId { get; set; }

		public int UserId { get; set; } // foreign key -> User
		public int ItemId { get; set; } // Product, Note, etc.
		public string ItemType { get; set; }
		public DateTime AddedDate { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User
	}
}
