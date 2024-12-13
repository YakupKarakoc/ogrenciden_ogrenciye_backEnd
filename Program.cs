using Microsoft.EntityFrameworkCore;
using ogrenciden_ogrenciye.Models;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabaný baðlantýsý için DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpContextAccessor ekle (gerekliyse)
builder.Services.AddHttpContextAccessor();

// CORS politikasý tanýmla
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();



// Geliþtirme ortamýnda hata ayýklama için DeveloperExceptionPage aktif et
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	// Üretim ortamýnda Swagger eriþimini sýnýrla
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
		options.RoutePrefix = string.Empty; // Root'ta Swagger açýlýr
	});
}



// Middleware'ler
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot eriþimi için gerekli
app.UseRouting(); // Explicit routing ekledik
app.UseAuthorization();

app.MapControllers();

app.Run();
