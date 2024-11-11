using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class NoteRating
	{
		[Key]
		public int RatingId { get; set; }

		public int UserId { get; set; } // foreign key -> User
		public int NoteId { get; set; } // foreign key -> Note
		public decimal RatingValue { get; set; }
		public string Review { get; set; }
		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property for User

		[ForeignKey("NoteId")]
		public Note Note { get; set; } // Navigation property for Note
	}
}
