using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class NoteRating
	{
		[Key]
		public int RatingId { get; set; }

		[Required]
		public int NoteId { get; set; } // Foreign key -> Note

		[Required]
		public int UserId { get; set; } // Foreign key -> User

		public decimal RatingValue { get; set; } // Rating değeri

		public string Review { get; set; } // Kullanıcı yorumu

		public DateTime Date { get; set; } = DateTime.UtcNow; // Tarih

		[ForeignKey("NoteId")]
		[InverseProperty("NoteRatings")]
		public Note Note { get; set; } // Navigation to Note

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation to User
	}
}
