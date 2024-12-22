using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class RoommateAd
	{
		[Key]
		public int AdId { get; set; }

		[Required]
		public int UserId { get; set; } // Ev sahibinin kullanıcı ID'si

		[ForeignKey("UserId")]
		public User User { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; } // İlan Başlığı

		[Required]
		public string Description { get; set; } // Açıklama

		[Required]
		[MaxLength(50)]
		public string City { get; set; } // Şehir

		[Required]
		[MaxLength(50)]
		public string District { get; set; } // İlçe

		[Required]
		public string RoomCount { get; set; } // Oda Sayısı (örn: 1+1, 2+1)

		[Required]
		public int SquareMeters { get; set; } // Ev Metrekaresi

		[Required]
		public string Furnishing { get; set; } // Eşyalı/Eşyasız

		[Required]
		public string GenderPreference { get; set; } // Arkadaş Tercihi

		[Required]
		[Column(TypeName = "decimal(10,2)")]
		public decimal RentPrice { get; set; } // Kira Ücreti

		public string ImagePath { get; set; } = "/images/default.jpg"; // Ev fotoğrafı

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma Tarihi
	}
}
