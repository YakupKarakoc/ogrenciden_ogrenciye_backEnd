using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class Note
	{
		[Key]
		public int NoteId { get; set; }

		public int UploaderId { get; set; } // foreign key -> User
		public string Subject { get; set; }
		public string Content { get; set; }
		public decimal Rating { get; set; }
		public DateTime UploadDate { get; set; }
		public bool TrendStatus { get; set; }

		[ForeignKey("UploaderId")]
		public User Uploader { get; set; } // Navigation property for User
	}
}
