using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ogrenciden_ogrenciye.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabaný baðlantýsý için DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpContextAccessor ekle
builder.Services.AddHttpContextAccessor();

// CORS politikasý tanýmla
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Geliþtirme ortamýnda Swagger ve hata ayýklama sayfasýný aktif et
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	
	
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
		options.RoutePrefix = "swagger"; // Root'ta Swagger açýlýr
	});
}
else
{
	// Üretim ortamýnda Swagger eriþimini sýnýrla
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
		options.RoutePrefix = "swagger"; 
	});
}


// Middleware'ler
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot eriþimi için
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
