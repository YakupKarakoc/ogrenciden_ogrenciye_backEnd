using Microsoft.AspNetCore.Mvc;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserSurveyController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserSurveyController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("{userId}")]
		public IActionResult GetSurvey(int userId)
		{
			var survey = _context.UserSurveys.FirstOrDefault(s => s.UserId == userId);
			if (survey == null)
			{
				// Eğer kullanıcıya ait bir anket yoksa, default değerlerle yeni bir kayıt oluştur
				survey = new UserSurvey
				{
					UserId = userId,
					Question1 = 3,
					Question2 = 3,
					Question3 = 3,
					Question4 = 3,
					Question5 = 3,
					Question6 = 3,
					Question7 = 3,
					Question8 = 3,
					Question9 = 3,
					Question10 = 3
				};
				_context.UserSurveys.Add(survey);
				_context.SaveChanges();
			}

			return Ok(survey);
		}


		[HttpPost("submit")]
		public IActionResult SubmitSurvey([FromBody] UserSurveyDto dto)
		{
			try
			{
				if (dto == null || dto.UserId <= 0)
				{
					return BadRequest("Geçersiz veri.");
				}

				var existingSurvey = _context.UserSurveys.FirstOrDefault(s => s.UserId == dto.UserId);

				if (existingSurvey != null)
				{
					// Mevcut anketi güncelle
					existingSurvey.Question1 = dto.Question1;
					existingSurvey.Question2 = dto.Question2;
					existingSurvey.Question3 = dto.Question3;
					existingSurvey.Question4 = dto.Question4;
					existingSurvey.Question5 = dto.Question5;
					existingSurvey.Question6 = dto.Question6;
					existingSurvey.Question7 = dto.Question7;
					existingSurvey.Question8 = dto.Question8;
					existingSurvey.Question9 = dto.Question9;
					existingSurvey.Question10 = dto.Question10;

					_context.UserSurveys.Update(existingSurvey);
				}
				else
				{
					// Yeni anket oluştur
					var newSurvey = new UserSurvey
					{
						UserId = dto.UserId,
						Question1 = dto.Question1,
						Question2 = dto.Question2,
						Question3 = dto.Question3,
						Question4 = dto.Question4,
						Question5 = dto.Question5,
						Question6 = dto.Question6,
						Question7 = dto.Question7,
						Question8 = dto.Question8,
						Question9 = dto.Question9,
						Question10 = dto.Question10,
					};

					_context.UserSurveys.Add(newSurvey);
				}

				_context.SaveChanges();
				return Ok("Anket başarıyla kaydedildi.");
			}
			catch (Exception ex)
			{
				// Detaylı hata mesajı döndür
				return StatusCode(500, $"Sunucu hatası: {ex.Message}");
			}
		}

	}
}
