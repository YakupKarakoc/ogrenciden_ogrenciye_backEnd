using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NoteFavoriteController : ControllerBase
	{
		private readonly AppDbContext _context;

		public NoteFavoriteController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> AddToFavorites([FromBody] NoteFavoriteCreateDTO noteFavoriteDto)
		{
			if (noteFavoriteDto == null) return BadRequest("Geçersiz veri.");

			var noteFavorite = new NoteFavorite
			{
				NoteId = noteFavoriteDto.NoteId,
				UserId = noteFavoriteDto.UserId
			};

			_context.NoteFavorites.Add(noteFavorite);
			await _context.SaveChangesAsync();

			return Ok("Not favorilere eklendi.");
		}



		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUserFavorites(int userId)
		{
			var favorites = await _context.NoteFavorites
				.Include(nf => nf.Note)
				.ThenInclude(note => note.Uploader)
				.Where(nf => nf.UserId == userId)
				.Select(nf => new NoteFavoriteDTO
				{
					NoteId = nf.NoteId,
					UserId = nf.UserId,
					Subject = nf.Note.Subject,
					Content = nf.Note.Content,
					FilePath = nf.Note.FilePath,
					UploaderName = $"{nf.Note.Uploader.FirstName} {nf.Note.Uploader.LastName}"
				})
				.ToListAsync();

			return Ok(favorites);
		}

		[HttpGet("search/{userId}")]
		public async Task<IActionResult> SearchUserFavorites(int userId, [FromQuery] string query)
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return BadRequest("Arama sorgusu boş olamaz.");
			}

			var favorites = await _context.NoteFavorites
				.Include(nf => nf.Note)
				.ThenInclude(note => note.Uploader)
				.Where(nf => nf.UserId == userId &&
							 (EF.Functions.Like(nf.Note.Subject, $"%{query}%") ||
							  EF.Functions.Like(nf.Note.Content, $"%{query}%")))
				.Select(nf => new NoteFavoriteDTO
				{
					NoteId = nf.NoteId,
					UserId = nf.UserId,
					Subject = nf.Note.Subject,
					Content = nf.Note.Content,
					FilePath = nf.Note.FilePath,
					UploaderName = $"{nf.Note.Uploader.FirstName} {nf.Note.Uploader.LastName}"
				})
				.ToListAsync();

			if (!favorites.Any())
			{
				return NotFound("Arama kriterine uygun favori not bulunamadı.");
			}

			return Ok(favorites);
		}






		// Favorilerden not kaldırma
		[HttpDelete("{userId}/{noteId}")]
		public async Task<IActionResult> RemoveFromFavorites(int userId, int noteId)
		{
			var favorite = await _context.NoteFavorites
				.FirstOrDefaultAsync(nf => nf.UserId == userId && nf.NoteId == noteId);

			if (favorite == null) return NotFound("Favori not bulunamadı.");

			_context.NoteFavorites.Remove(favorite);
			await _context.SaveChangesAsync();

			return Ok("Favorilerden kaldırıldı.");
		}
	}
}
