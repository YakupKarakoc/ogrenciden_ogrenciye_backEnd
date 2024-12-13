using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Favorite
	{
		[Key]
		public int FavoriteId { get; set; }

		public int UserId { get; set; } // Foreign key -> User

		public int ItemId { get; set; } // Product or other types

		public string ItemType { get; set; }

		public DateTime AddedDate { get; set; }

		[ForeignKey("ItemId")]
		public Product Product { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
