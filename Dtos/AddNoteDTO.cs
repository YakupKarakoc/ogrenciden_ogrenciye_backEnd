namespace ogrenciden_ogrenciye.Models.Dtos
{
	public class AddNoteDTO
	{
		public int UploaderId { get; set; } // Kullanıcı Kimliği
		public string Subject { get; set; } // Ders Adı
		public string Content { get; set; } // Not Açıklaması
		public IFormFile File { get; set; } // PDF Dosyası
	}
}
