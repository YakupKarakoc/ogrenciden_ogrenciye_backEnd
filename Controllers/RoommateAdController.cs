using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ogrenciden_ogrenciye.Controllers
{
	[ApiController]
	[Route("api/RoommateAds")]
	public class RoommateAdController : ControllerBase
	{
		private readonly AppDbContext _context;

		public RoommateAdController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Route("AddWithImage")]
		public async Task<IActionResult> AddRoommateAdWithImage([FromForm] RoommateAdDto roommateAdDto, IFormFile image)
		{
			if (roommateAdDto == null || string.IsNullOrEmpty(roommateAdDto.Title) || roommateAdDto.UserId == 0)
			{
				return BadRequest("Eksik veya geçersiz ilan bilgisi.");
			}

			var user = _context.Users.FirstOrDefault(u => u.Id == roommateAdDto.UserId);
			if (user == null)
			{
				return NotFound("Kullanıcı bulunamadı.");
			}

			string imagePath = "/images/roomMate/default.jpg"; // Varsayılan resim
			if (image != null)
			{
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/roomMate");
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
				var filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await image.CopyToAsync(stream);
				}

				imagePath = "/images/roomMate/" + uniqueFileName;
			}

			var roommateAd = new RoommateAd
			{
				Title = roommateAdDto.Title,
				Description = roommateAdDto.Description,
				City = roommateAdDto.City,
				District = roommateAdDto.District,
				RoomCount = roommateAdDto.RoomCount,
				SquareMeters = roommateAdDto.SquareMeters,
				Furnishing = roommateAdDto.Furnishing,
				GenderPreference = roommateAdDto.GenderPreference,
				RentPrice = roommateAdDto.RentPrice,
				ImagePath = imagePath,
				CreatedAt = DateTime.UtcNow,
				UserId = roommateAdDto.UserId,
				User = user
			};

			_context.RoommateAds.Add(roommateAd);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetAdById), new { id = roommateAd.AdId }, roommateAd);
		}

		[HttpGet]
		[Route("All")]
		public IActionResult GetAllAds()
		{
			var ads = _context.RoommateAds.Include(r => r.User).ToList();
			return Ok(ads);
		}

		[HttpGet("Recommended")]
		public IActionResult GetRecommendedAds([FromQuery] int userId)
		{
			var userSurvey = _context.UserSurveys.FirstOrDefault(us => us.UserId == userId);
			if (userSurvey == null)
			{
				return BadRequest(new { success = false, message = "Kullanıcı anket cevapları bulunamadı." });
			}

			var otherUsersSurveys = _context.UserSurveys
				.Where(us => us.UserId != userId)
				.ToList();

			var matchedUserIds = new List<int>();

			foreach (var otherSurvey in otherUsersSurveys)
			{
				// Mutlak fark toplamını hesapla
				double totalDifference = 0;
				totalDifference += Math.Abs(userSurvey.Question1 - otherSurvey.Question1);
				totalDifference += Math.Abs(userSurvey.Question2 - otherSurvey.Question2);
				totalDifference += Math.Abs(userSurvey.Question3 - otherSurvey.Question3);
				totalDifference += Math.Abs(userSurvey.Question4 - otherSurvey.Question4);
				totalDifference += Math.Abs(userSurvey.Question5 - otherSurvey.Question5);
				totalDifference += Math.Abs(userSurvey.Question6 - otherSurvey.Question6);
				totalDifference += Math.Abs(userSurvey.Question7 - otherSurvey.Question7);
				totalDifference += Math.Abs(userSurvey.Question8 - otherSurvey.Question8);
				totalDifference += Math.Abs(userSurvey.Question9 - otherSurvey.Question9);
				totalDifference += Math.Abs(userSurvey.Question10 - otherSurvey.Question10);

				// Oran hesapla: (1 - (toplam fark / 40))
				double matchPercentage = 1 - (totalDifference / 40);
				Console.WriteLine($"Toplam Fark: {totalDifference}, Oran: {matchPercentage}");

				// Eğer oran %50 veya daha büyükse, eşleşen kullanıcılar listesine ekle
				if (matchPercentage >= 0.5)
				{
					matchedUserIds.Add(otherSurvey.UserId);
					Console.WriteLine($"Eşleşen Kullanıcı ID: {otherSurvey.UserId}");
				}
			}

			// Eşleşen kullanıcıların ilanlarını al
			var recommendedAds = _context.RoommateAds
				.Where(ad => matchedUserIds.Contains(ad.UserId))
				.ToList();

			if (!recommendedAds.Any())
			{
				return Ok(new { success = true, message = "Önerilen ilan bulunamadı." });
			}

			return Ok(recommendedAds);
		}


		[HttpGet]
		[Route("{id}")]
		public IActionResult GetAdById(int id)
		{
			var ad = _context.RoommateAds.Include(r => r.User).FirstOrDefault(r => r.AdId == id);
			if (ad == null)
				return NotFound(new { success = false, message = "İlan bulunamadı." });

			return Ok(ad);
		}

		[HttpDelete]
		[Route("{id}")]
		public IActionResult DeleteAd(int id)
		{
			var ad = _context.RoommateAds.Find(id);

			if (ad == null)
				return NotFound(new { success = false, message = "İlan bulunamadı." });

			try
			{
				_context.RoommateAds.Remove(ad);
				_context.SaveChanges();
				return Ok(new { success = true, message = "İlan başarıyla silindi." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "İlan silinirken bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet("Search")]
		public IActionResult SearchRoommateAds([FromQuery] string? city, [FromQuery] string? roomCount, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
		{
			var ads = _context.RoommateAds.AsQueryable();

			if (!string.IsNullOrEmpty(city))
			{
				ads = ads.Where(ad => ad.City.ToLower().Contains(city.ToLower()));
			}

			if (!string.IsNullOrEmpty(roomCount))
			{
				ads = ads.Where(ad => ad.RoomCount == roomCount);
			}

			if (minPrice.HasValue)
			{
				ads = ads.Where(ad => ad.RentPrice >= minPrice.Value);
			}

			if (maxPrice.HasValue)
			{
				ads = ads.Where(ad => ad.RentPrice <= maxPrice.Value);
			}

			return Ok(ads.ToList());
		}





	}
}
