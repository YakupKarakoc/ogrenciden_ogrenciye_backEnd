using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
	private readonly AppDbContext _context;

	public FavoritesController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult GetFavorites([FromQuery] string userEmail)
	{
		if (string.IsNullOrEmpty(userEmail))
		{
			return BadRequest("User email is required.");
		}

		var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var favorites = _context.Favorites
		.Include(f => f.Product)
		.ThenInclude(p => p.Seller) // Seller bilgilerini dahil et
		.Where(f => f.UserId == user.Id)
		.Select(f => new
		{
			f.FavoriteId,
			f.AddedDate,
			Product = new
			{
				f.Product.ProductId,
				f.Product.Title,
				f.Product.Description,
				f.Product.Price,
				f.Product.ImagePath,
				SellerName = f.Product.Seller != null
					? f.Product.Seller.FirstName + " " + f.Product.Seller.LastName
					: "Bilinmeyen Satıcı" // Satıcı bilgisi eksikse
			}
		})
		.ToList();




		return Ok(favorites);
	}

	[HttpPost]
	public IActionResult AddFavorite([FromBody] FavoriteDto favoriteDto)
	{
		if (favoriteDto == null || string.IsNullOrEmpty(favoriteDto.UserEmail) || favoriteDto.ItemId == 0)
		{
			return BadRequest("Invalid favorite data.");
		}

		var user = _context.Users.FirstOrDefault(u => u.Email == favoriteDto.UserEmail);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var favorite = new Favorite
		{
			UserId = user.Id,
			ItemId = favoriteDto.ItemId,
			ItemType = favoriteDto.ItemType,
			AddedDate = DateTime.UtcNow
		};
		_context.Favorites.Add(favorite);
		_context.SaveChanges();

		return CreatedAtAction(nameof(GetFavorites), new { userEmail = favoriteDto.UserEmail }, favorite);
	}

	[HttpDelete("{userEmail}/{itemId}")]
	public IActionResult RemoveFavorite(string userEmail, int itemId)
	{
		var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var favorite = _context.Favorites
			.FirstOrDefault(f => f.UserId == user.Id && f.ItemId == itemId);

		if (favorite == null)
		{
			return NotFound("Favorite not found.");
		}

		_context.Favorites.Remove(favorite);
		_context.SaveChanges();
		return Ok("Favorite removed successfully.");
	}
}