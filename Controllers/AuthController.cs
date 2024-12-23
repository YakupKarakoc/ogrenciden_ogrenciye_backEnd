﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ogrenciden_ogrenciye.Models;
using System.Linq;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly PasswordHasher<User> _passwordHasher;

		public AuthController(AppDbContext context)
		{
			_context = context;
			_passwordHasher = new PasswordHasher<User>();
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] RegisterModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			// Kullanıcı var mı kontrol et
			if (_context.Users.Any(u => u.Email == model.Email))
				return BadRequest(new { success = false, message = "Bu e-posta zaten kullanılıyor." });

			var user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				Gender = model.Gender
			};

			// Şifreyi hashle
			user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

			_context.Users.Add(user);
			_context.SaveChanges();

			return Ok(new { success = true, message = "Kayıt başarıyla tamamlandı!" });
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

			if (user != null)
			{
				var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

				if (result == PasswordVerificationResult.Success)
				{
					// Başarıyla giriş yaptı
					return Ok(new
					{
						success = true,
						token = "fake-jwt-token",
						name = user.FirstName,
						userId = user.Id,
						email = user.Email
					});
				}
			}

			return Unauthorized(new { success = false, message = "Geçersiz kullanıcı adı veya şifre." });
		}

		[HttpGet("profile/{email}")]
		public IActionResult GetUserProfile(string email)
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == email);
			if (user == null)
				return NotFound(new { success = false, message = "Kullanıcı bulunamadı." });

			return Ok(new
			{
				success = true,
				data = new
				{
					user.FirstName,
					user.LastName,
					user.Email,
					user.Gender,
					user.PhoneNumber
				}
			});
		}

		[HttpPut("profile/update")]
		public IActionResult UpdateUserProfile([FromBody] UpdateProfileModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
			if (user == null)
				return NotFound(new { success = false, message = "Kullanıcı bulunamadı." });

			if (!string.IsNullOrEmpty(model.PhoneNumber))
				user.PhoneNumber = model.PhoneNumber;

			if (!string.IsNullOrEmpty(model.NewPassword))
				user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);

			if (string.IsNullOrEmpty(model.PhoneNumber) && string.IsNullOrEmpty(model.NewPassword))
			{
				return BadRequest(new { success = false, message = "Güncelleme için telefon numarası veya şifre alanlarından en az birini doldurun!" });
			}


			try
			{
				if (!string.IsNullOrEmpty(model.PhoneNumber))
					user.PhoneNumber = model.PhoneNumber;

				if (!string.IsNullOrEmpty(model.NewPassword))
					user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);

				_context.SaveChanges();
				return Ok(new { success = true, message = "Profil başarıyla güncellendi." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "Bir hata oluştu. Lütfen tekrar deneyin.", error = ex.Message });
			}

		}
	}
}