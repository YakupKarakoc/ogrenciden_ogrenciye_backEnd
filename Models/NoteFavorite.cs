using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class NoteFavorite
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int UserId { get; set; } // Foreign Key -> User

		[Required]
		public int NoteId { get; set; } // Foreign Key -> Note

		[ForeignKey("UserId")]
		public User User { get; set; } // Navigation property

		[ForeignKey("NoteId")]
		public Note Note { get; set; } // Navigation property
	}

}
