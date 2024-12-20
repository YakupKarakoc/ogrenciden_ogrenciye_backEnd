using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Models;
using ogrenciden_ogrenciye.Models.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NoteController : ControllerBase
	{
		private readonly AppDbContext _context;

		public NoteController(AppDbContext context)
		{
			_context = context;
		}

		// Not ekleme işlevi
		[HttpPost]
		public async Task<IActionResult> AddNote([FromForm] AddNoteDTO noteDto)
		{
			if (noteDto == null || string.IsNullOrEmpty(noteDto.Subject) || string.IsNullOrEmpty(noteDto.Content))
			{
				return BadRequest(new { message = "Gerekli tüm alanları doldurun." });
			}

			try
			{
				string filePath = null;

				// PDF dosyasını kaydetme
				if (noteDto.File != null)
				{
					var fileName = $"{Guid.NewGuid()}_{noteDto.File.FileName}";
					var fullPath = Path.Combine("wwwroot/notes", fileName);

					if (!Directory.Exists("wwwroot/notes"))
					{
						Directory.CreateDirectory("wwwroot/notes");
					}

					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						await noteDto.File.CopyToAsync(stream);
					}

					filePath = $"/notes/{fileName}";
				}

				// Note nesnesini oluşturma
				var note = new Note
				{
					UploaderId = noteDto.UploaderId,
					Subject = noteDto.Subject,
					Content = noteDto.Content,
					FilePath = filePath,
					UploadDate = DateTime.UtcNow
				};

				_context.Notes.Add(note);
				await _context.SaveChangesAsync();

				return Ok(new { message = "Not başarıyla eklendi!", note });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		// Notları arama işlevi
		[HttpGet("search")]
		public IActionResult SearchNotes([FromQuery] string query)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(query))
				{
					return BadRequest(new { message = "Arama sorgusu boş olamaz." });
				}

				var notes = _context.Notes
					.Include(n => n.Uploader) // Uploader ilişkisini dahil et
					.Where(n => EF.Functions.Like(n.Subject, $"%{query}%") || EF.Functions.Like(n.Content, $"%{query}%"))
					.Select(n => new NoteDTO
					{
						NoteId = n.NoteId,
						UploaderId = n.UploaderId,
						UploaderName = n.Uploader != null ? $"{n.Uploader.FirstName} {n.Uploader.LastName}" : "Bilinmiyor",
						Subject = n.Subject,
						Content = n.Content,
						FilePath = n.FilePath,
						UploadDate = n.UploadDate
					})
					.ToList();

				return Ok(notes);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		// Belirli bir kullanıcıya ait notları getir
		[HttpGet("user/{userId}")]
		public IActionResult GetUserNotes(int userId)
		{
			try
			{
				var userNotes = _context.Notes
					.Include(n => n.Uploader) // Uploader ilişkisini dahil et
					.Where(n => n.UploaderId == userId) // UploaderId'ye göre filtrele
					.Select(n => new NoteDTO
					{
						NoteId = n.NoteId,
						UploaderId = n.UploaderId,
						UploaderName = n.Uploader != null ? $"{n.Uploader.FirstName} {n.Uploader.LastName}" : "Bilinmiyor",
						Subject = n.Subject,
						Content = n.Content,
						FilePath = n.FilePath,
						UploadDate = n.UploadDate
					})
					.ToList();

				return Ok(userNotes);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		//deneme

		// Notları listeleme işlevi
		[HttpGet]
		public IActionResult GetNotes()
		{
			try
			{
				var notes = _context.Notes
					.Include(n => n.Uploader) // Uploader ilişkisini dahil et
					.Select(n => new NoteDTO
					{
						NoteId = n.NoteId,
						UploaderId = n.UploaderId,
						UploaderName = n.Uploader != null ? $"{n.Uploader.FirstName} {n.Uploader.LastName}" : "Bilinmiyor",
						Subject = n.Subject,
						Content = n.Content,
						FilePath = n.FilePath,
						UploadDate = n.UploadDate
					})
					.ToList();

				return Ok(notes);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}
	}
}
