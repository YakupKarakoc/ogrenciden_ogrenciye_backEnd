namespace ogrenciden_ogrenciye.Dtos
{
	public class RoommateAdDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string City { get; set; } // Şehir
		public string District { get; set; } // İlçe
		public string RoomCount { get; set; } // Oda Sayısı
		public int SquareMeters { get; set; } // Metrekare
		public string Furnishing { get; set; } // Eşyalı/Eşyasız
		public string GenderPreference { get; set; } // Arkadaş Tercihi
		public decimal RentPrice { get; set; } // Kira Ücreti
		public int UserId { get; set; } // Ev sahibinin kullanıcı ID'si
	}
}
