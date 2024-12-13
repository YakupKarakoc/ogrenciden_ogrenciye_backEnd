using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class NoteRating
	{
		[Key]
		public int RatingId { get; set; }

		public int UserId { get; set; } // Foreign key -> User
		public int NoteId { get; set; } // Foreign key -> Note
		public decimal RatingValue { get; set; }
		public string Review { get; set; }
		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		[InverseProperty("NoteRatings")]
		public User User { get; set; } // Navigation property for User

		[ForeignKey("NoteId")]
		[InverseProperty("NoteRatings")]
		public Note Note { get; set; } // Navigation property for Note
	}
}
