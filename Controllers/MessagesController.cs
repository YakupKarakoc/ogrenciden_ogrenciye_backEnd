using Microsoft.AspNetCore.Mvc;
using ogrenciden_ogrenciye.Models;
using System.Linq;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public MessagesController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult SendMessage([FromBody] Message message)
		{
			if (!ModelState.IsValid)
				return BadRequest(new { success = false, message = "Geçersiz veri." });

			_context.Messages.Add(message);
			_context.SaveChanges();

			return Ok(new { success = true, message = "Mesaj başarıyla gönderildi." });
		}

		[HttpGet("{userId}/{chatUserId}")]
		public IActionResult GetMessages(int userId, int chatUserId)
		{
			var messages = _context.Messages
				.Where(m => (m.SenderId == userId && m.ReceiverId == chatUserId) ||
							(m.SenderId == chatUserId && m.ReceiverId == userId))
				.OrderBy(m => m.SentAt)
				.ToList();

			return Ok(new { success = true, messages });
		}
	}
}

