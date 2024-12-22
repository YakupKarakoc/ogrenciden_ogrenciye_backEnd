using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductFavoriteController : ControllerBase
{
	private readonly AppDbContext _context;

	public ProductFavoriteController(AppDbContext context)
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

		var favorites = _context.ProductFavorites
			.Include(f => f.Product)
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
					f.Product.ImagePath
				}
			})
			.ToList();

		return Ok(favorites);
	}

	[HttpPost]
	public IActionResult AddFavorite([FromBody] FavoriteDto favoriteDto)
	{
		if (favoriteDto == null || string.IsNullOrEmpty(favoriteDto.UserEmail) || favoriteDto.ProductId == 0)
		{
			return BadRequest("Invalid favorite data.");
		}

		var user = _context.Users.FirstOrDefault(u => u.Email == favoriteDto.UserEmail);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var favorite = new ProductFavorite
		{
			UserId = user.Id,
			ProductId = favoriteDto.ProductId,
			AddedDate = DateTime.UtcNow
		};

		_context.ProductFavorites.Add(favorite);
		_context.SaveChanges();

		return CreatedAtAction(nameof(GetFavorites), new { userEmail = favoriteDto.UserEmail }, favorite);
	}

	[HttpDelete("{userEmail}/{productId}")]
	public IActionResult RemoveFavorite(string userEmail, int productId)
	{
		var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
		if (user == null)
		{
			return NotFound("User not found.");
		}

		var favorite = _context.ProductFavorites
			.FirstOrDefault(f => f.UserId == user.Id && f.ProductId == productId);

		if (favorite == null)
		{
			return NotFound("Favorite not found.");
		}

		_context.ProductFavorites.Remove(favorite);
		_context.SaveChanges();
		return Ok("Favorite removed successfully.");
	}
}
