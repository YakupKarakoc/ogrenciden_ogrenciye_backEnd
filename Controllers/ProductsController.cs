using Microsoft.AspNetCore.Mvc;
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

				// Satıcıyı veritabanında bul
				var user = _context.Users.FirstOrDefault(u => u.Email == productDto.SellerEmail);
				if (user == null)
				{
					return BadRequest(new { message = "Satıcı bulunamadı." });
				}

				// Yeni Product nesnesi oluştur ve DTO'dan değerleri ata
				var product = new Product
				{
					Title = productDto.Title,
					Description = productDto.Description,
					Price = productDto.Price,
					Category = productDto.Category,
					SellerId = user.Id,
					SellerEmail = user.Email,
					CreatedAt = DateTime.UtcNow
				};

				// Görsel işleme
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

				// Veritabanına kaydet
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


		// Tüm Ürünleri Listeleme
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

		// Belirli Bir Ürünü Getirme
		[HttpGet("{id}")]
		public IActionResult GetProductById(int id)
		{
			try
			{
				var product = _context.Products
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

		// Ürün Silme
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
			Console.WriteLine($"Gelen sellerEmail: {sellerEmail}"); // Gelen parametreyi kontrol edin
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

				if (userAds.Count == 0)
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


	}
}
