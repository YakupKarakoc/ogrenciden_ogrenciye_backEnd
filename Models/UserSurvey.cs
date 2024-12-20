﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ogrenciden_ogrenciye.Models
{
	public class UserSurvey
	{
		[Key]
		public int SurveyId { get; set; }

		[Required]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[Range(1, 5)]
		public int Question1 { get; set; }

		[Range(1, 5)]
		public int Question2 { get; set; }

		[Range(1, 5)]
		public int Question3 { get; set; }

		[Range(1, 5)]
		public int Question4 { get; set; }

		[Range(1, 5)]
		public int Question5 { get; set; }

		// Yeni sorular
		[Range(1, 5)]
		public int Question6 { get; set; }

		[Range(1, 5)]
		public int Question7 { get; set; }

		[Range(1, 5)]
		public int Question8 { get; set; }

		[Range(1, 5)]
		public int Question9 { get; set; }

		[Range(1, 5)]
		public int Question10 { get; set; }
	}
}
