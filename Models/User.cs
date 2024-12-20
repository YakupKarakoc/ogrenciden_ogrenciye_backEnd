using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ogrenciden_ogrenciye.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		public string PhoneNumber { get; set; }
		public string Gender { get; set; }

		public ICollection<Note> Notes { get; set; }
		public ICollection<NoteRating> NoteRatings { get; set; }
	}
}