using System.ComponentModel.DataAnnotations;

namespace ogrenciden_ogrenciye.Models
{
	public class RegisterModel
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
		public string Password { get; set; }

		public string PhoneNumber { get; set; }
		public string Gender { get; set; }
	}
}
