namespace ogrenciden_ogrenciye.Models.Dtos
{
	public class NoteDTO
	{
		public int NoteId { get; set; } // Not Kimliği
		public int UploaderId { get; set; } // Kullanıcı Kimliği
		public string UploaderName { get; set; } // Kullanıcı Adı
		public string Subject { get; set; } // Ders Adı
		public string Content { get; set; } // Not Açıklaması
		public string FilePath { get; set; } // Dosya Yolu
		public DateTime UploadDate { get; set; } // Yüklenme Tarihi
	}
}

