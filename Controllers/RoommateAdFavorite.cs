using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Include metodu için gerekli
using ogrenciden_ogrenciye.Models;

namespace ogrenciden_ogrenciye.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RoommateAdFavoriteController : ControllerBase
	{
		private readonly AppDbContext _context;

		public RoommateAdFavoriteController(AppDbContext context)
		{
			_context = context;
		}

		// Favori ekle
		[HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddFavorite([FromBody] RoommateAdFavorite favorite)
		{
			if (favorite == null)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			try
			{
				_context.RoommateAdFavorites.Add(favorite);
				await _context.SaveChangesAsync();
				return Ok(new { success = true, message = "Favori eklendi." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "Favori eklenirken hata oluştu.", error = ex.Message });
			}
		}

		// Favoriler listesini al
		[HttpGet]
		[Route("All")]
		public IActionResult GetAllFavorites([FromQuery] int userId)
		{
			var favorites = _context.RoommateAdFavorites
				.Where(f => f.UserId == userId)
				.Include(f => f.RoommateAd) // Burada gerekli olan Microsoft.EntityFrameworkCore namespace
				.ToList();

			return Ok(favorites);
		}

		// Favori sil
		[HttpDelete]
		[Route("Delete/{favoriteId}")]
		public async Task<IActionResult> DeleteFavorite(int favoriteId)
		{
			var favorite = await _context.RoommateAdFavorites.FindAsync(favoriteId);

			if (favorite == null)
				return NotFound(new { success = false, message = "Favori bulunamadı." });

			try
			{
				_context.RoommateAdFavorites.Remove(favorite);
				await _context.SaveChangesAsync();
				return Ok(new { success = true, message = "Favori silindi." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "Favori silinirken hata oluştu.", error = ex.Message });
			}
		}
	}
}
