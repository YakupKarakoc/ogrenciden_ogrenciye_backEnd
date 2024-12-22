using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class UserPreferences
	{
		[Key]
		public int PreferenceId { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public decimal Budget { get; set; }

		[Required]
		[MaxLength(10)]
		public string Gender { get; set; }

		[Required]
		public string Features { get; set; } // Özellikler virgülle ayrılmış bir string

		[ForeignKey("UserId")]
		public User User { get; set; }
	}


}
