using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Models;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritaban� ba�lant�s� i�in DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpContextAccessor ekle (gerekliyse)
builder.Services.AddHttpContextAccessor();

// CORS politikas� tan�mla
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();



// Geli�tirme ortam�nda hata ay�klama i�in DeveloperExceptionPage aktif et
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	// �retim ortam�nda Swagger eri�imini s�n�rla
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
		options.RoutePrefix = string.Empty; // Root'ta Swagger a��l�r
	});
}



// Middleware'ler
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot eri�imi i�in gerekli
app.UseRouting(); // Explicit routing ekledik
app.UseAuthorization();

app.MapControllers();

app.Run();
