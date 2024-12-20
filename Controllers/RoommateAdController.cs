using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Models;
using System.Linq;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoommateAdController : ControllerBase
	{
		private readonly AppDbContext _context;

		public RoommateAdController(AppDbContext context)
		{
			_context = context;
		}

		// Tüm ilanları getir
		[HttpGet]
		public IActionResult GetAllAds()
		{
			var ads = _context.RoommateAds.Include(r => r.User).ToList();
			return Ok(ads);
		}

		// Kullanıcıya özel öneriler getir
		[HttpGet("recommendations/{userId}")]
		public IActionResult GetRecommendations(int userId)
		{
			var survey = _context.UserSurveys.FirstOrDefault(s => s.UserId == userId);
			if (survey == null)
			{
				return NotFound("User survey not found.");
			}

			// Örnek bir eşleştirme algoritması
			var recommendedAds = _context.RoommateAds
				.Where(ad =>
					(survey.Question1 >= 4 && ad.Features.Contains("Sessiz")) ||
					(survey.Question2 >= 4 && ad.Features.Contains("Evcil Hayvan Dostu")))
				.ToList();

			return Ok(recommendedAds);
		}

		// Yeni ilan ekle
		[HttpPost]
		public IActionResult CreateAd([FromBody] RoommateAd newAd)
		{
			if (newAd == null)
			{
				return BadRequest("Invalid ad data.");
			}

			_context.RoommateAds.Add(newAd);
			_context.SaveChanges();

			return Ok("Ad created successfully.");
		}

		// İlan detaylarını getir
		[HttpGet("{id}")]
		public IActionResult GetAdById(int id)
		{
			var ad = _context.RoommateAds.Include(r => r.User).FirstOrDefault(a => a.AdId == id);
			if (ad == null)
			{
				return NotFound("Ad not found.");
			}
			return Ok(ad);
		}
	}
}
