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

// Veritaban� ba�lant�s� i�in DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpContextAccessor ekle
builder.Services.AddHttpContextAccessor();

// CORS politikas� tan�mla
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Geli�tirme ortam�nda Swagger ve hata ay�klama sayfas�n� aktif et
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	
	
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
		options.RoutePrefix = "swagger"; // Root'ta Swagger a��l�r
	});
}
else
{
	// �retim ortam�nda Swagger eri�imini s�n�rla
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
app.UseStaticFiles(); // wwwroot eri�imi i�in
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
