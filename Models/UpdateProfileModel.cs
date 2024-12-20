using System.ComponentModel.DataAnnotations;

namespace ogrenciden_ogrenciye.Models
{
	public class UpdateProfileModel
	{
		[Required, EmailAddress]
		public string Email { get; set; }

		[Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
		public string? PhoneNumber { get; set; } // Null kabul eder

		[MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
		public string? NewPassword { get; set; } // Null kabul eder
	}
}
