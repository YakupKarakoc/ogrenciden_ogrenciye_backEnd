using Microsoft.AspNetCore.Identity;
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

			// Yeni kullanıcı oluştur
			var user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber, // Telefon numarasını ekledik
				Gender = model.Gender            // Cinsiyeti ekledik
			};

			// Şifreyi hashleyin
			user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

			_context.Users.Add(user);
			_context.SaveChanges();

			return Ok(new { success = true });
		}


		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

			if (user != null)
			{
				// Şifrenin doğruluğunu kontrol edin
				var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

				if (result == PasswordVerificationResult.Success)
				{
					return Ok(new { success = true, token = "fake-jwt-token" });
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

			_context.SaveChanges();
			return Ok(new { success = true, message = "Profil başarıyla güncellendi." });
		}



	}
}
