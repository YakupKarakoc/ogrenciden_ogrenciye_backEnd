namespace ogrenciden_ogrenciye.Dtos
{
	public class NoteFavoriteDTO
	{
		public int NoteId { get; set; }
		public int UserId { get; set; }
		public string Subject { get; set; } // Ders Adı
		public string Content { get; set; } // Açıklama
		public string FilePath { get; set; } // PDF Dosya Yolu
		public string UploaderName { get; set; } // Notu Ekleyen
	}
}
