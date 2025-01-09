using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Dtos;
using ogrenciden_ogrenciye.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ogrenciden_ogrenciye.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductsController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto)
		{
			try
			{
				if (string.IsNullOrEmpty(productDto.SellerEmail))
				{
					return BadRequest(new { message = "Satıcı e-posta adresi gerekli." });
				}

				var user = _context.Users.FirstOrDefault(u => u.Email == productDto.SellerEmail);
				if (user == null)
				{
					return BadRequest(new { message = "Satıcı bulunamadı." });
				}

				var product = new Product
				{
					Title = productDto.Title,
					Description = productDto.Description,
					Price = productDto.Price,
					Category = productDto.Category,
					SubCategory = productDto.SubCategory,
					SellerId = user.Id,
					SellerEmail = user.Email,
					CreatedAt = DateTime.UtcNow
				};

				if (productDto.Image != null)
				{
					var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
					var fileExtension = Path.GetExtension(productDto.Image.FileName).ToLower();

					if (!allowedExtensions.Contains(fileExtension))
					{
						return BadRequest(new { message = "Sadece .jpg, .jpeg ve .png formatındaki dosyalara izin veriliyor." });
					}

					var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
					if (!Directory.Exists(imagesFolder))
					{
						Directory.CreateDirectory(imagesFolder);
					}

					var fileName = Guid.NewGuid().ToString() + fileExtension;
					var filePath = Path.Combine(imagesFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await productDto.Image.CopyToAsync(stream);
					}

					product.ImagePath = $"/images/{fileName}";
				}
				else
				{
					product.ImagePath = "/images/default.jpg";
				}

				_context.Products.Add(product);
				await _context.SaveChangesAsync();

				return Ok(new { message = "Ürün başarıyla eklendi!", product });
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Hata: {ex.Message}");
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet]
		public IActionResult GetAllProducts()
		{
			try
			{
				var products = _context.Products
					.Select(p => new
					{
						p.ProductId,
						p.Title,
						p.Description,
						p.Price,
						p.Category,
						p.ImagePath,
						SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
						p.CreatedAt
					})
					.ToList();

				return Ok(products);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetProductById(int id)
		{
			try
			{
				var product = _context.Products
					.Include(p => p.Seller) // Seller ilişkisini dahil ediyoruz
					.Where(p => p.ProductId == id)
					.Select(p => new
					{
						p.ProductId,
						p.Title,
						p.Description,
						p.Price,
						p.Category,
						p.ImagePath,
						SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
						SellerPhone = p.Seller.PhoneNumber, // Telefon numarası ekleniyor
						p.CreatedAt
					})
					.FirstOrDefault();

				if (product == null)
				{
					return NotFound(new { message = "Ürün bulunamadı." });
				}

				return Ok(product);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}


		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			try
			{
				var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
				if (product == null)
				{
					return NotFound(new { message = "Ürün bulunamadı." });
				}

				_context.Products.Remove(product);
				_context.SaveChanges();

				return Ok(new { message = "Ürün başarıyla silindi!" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet("MyAds")]
		public IActionResult GetUserAds(string sellerEmail)
		{
			try
			{
				var userAds = _context.Products
					.Where(p => p.SellerEmail == sellerEmail)
					.Select(p => new
					{
						p.ProductId,
						p.Title,
						p.Description,
						p.Price,
						p.Category,
						p.ImagePath,
						SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
						p.CreatedAt
					})
					.ToList();

				if (!userAds.Any())
				{
					return NotFound(new { message = "Henüz bir ilan verilmedi." });
				}

				return Ok(userAds);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet("category/{category}/{subCategory?}")]
		public IActionResult GetProductsByCategory(string category, string subCategory = null, int page = 1, int pageSize = 10)
		{
			try
			{
				var query = _context.Products.AsQueryable();

				// Kategoriye göre filtreleme
				query = query.Where(p => EF.Functions.Like(p.Category, $"%{category}%"));

				// Alt kategoriye göre filtreleme
				if (!string.IsNullOrEmpty(subCategory))
				{
					query = query.Where(p => EF.Functions.Like(p.SubCategory, $"%{subCategory}%"));
				}

				// Sıralama ve sayfalama
				var products = query
					.OrderByDescending(p => p.CreatedAt)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.Select(p => new
					{
						p.ProductId,
						p.Title,
						p.Description,
						p.Price,
						p.Category,
						p.SubCategory,
						p.ImagePath,
						SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
						p.CreatedAt
					})
					.ToList();

				if (!products.Any())
				{
					return NotFound(new { message = "Bu kategoride ürün bulunamadı." });
				}

				return Ok(products);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}

		[HttpGet("Search")]
		public IActionResult SearchProducts(string query)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(query))
				{
					return BadRequest(new { message = "Arama sorgusu boş olamaz." });
				}

				// Arama kriterlerini kontrol et
				var searchResults = _context.Products
					.Where(p => EF.Functions.Like(p.Title, $"%{query}%") ||
								EF.Functions.Like(p.Description, $"%{query}%") ||
								EF.Functions.Like(p.Category, $"%{query}%") ||
								EF.Functions.Like(p.SubCategory, $"%{query}%"))
					.Select(p => new
					{
						p.ProductId,
						p.Title,
						p.Description,
						p.Price,
						p.Category,
						p.SubCategory,
						p.ImagePath,
						SellerName = p.Seller.FirstName + " " + p.Seller.LastName,
						p.CreatedAt
					})
					.ToList();

				if (!searchResults.Any())
				{
					return NotFound(new { message = "Aradığınız kritere uygun bir ürün bulunamadı." });
				}

				return Ok(searchResults);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
			}
		}


	}
}
