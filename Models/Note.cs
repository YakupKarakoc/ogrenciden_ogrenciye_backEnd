using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ogrenciden_ogrenciye.Models
{
	public class Note
	{
		[Key]
		public int NoteId { get; set; }

		[Required]
		public int UploaderId { get; set; } // Foreign key -> User

		[Required]
		public string Subject { get; set; } // Ders Adı

		[Required]
		public string Content { get; set; } // Açıklama

		public string FilePath { get; set; } // PDF dosyası yolu

		public DateTime UploadDate { get; set; } = DateTime.UtcNow; // Yükleme tarihi

		[ForeignKey("UploaderId")]
		public User Uploader { get; set; } // Navigation property for User

		[InverseProperty("Note")]
		[JsonIgnore]
		public ICollection<NoteRating> NoteRatings { get; set; } // Navigation to NoteRating
	}
}
