using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentCar.Domain.Entities;
using RentCar.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext - SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RentCar.API",
        Version = "v1",
        Description = "Rent-A-Car API",
        Contact = new OpenApiContact
        {
            Name = "RentCar Team",
            Email = "support@rentcar.com"
        }
    });
});

// CORS - React için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")  // React portu
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentCar.API v1");
    });
}

// HTTP kullanıyoruz, HTTPS redirection'ı kaldır
// app.UseHttpsRedirection();

// CORS'u kullan
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

// Database'i otomatik oluştur ve test verisi ekle
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    
    if (!dbContext.Cars.Any())
    {
        dbContext.Cars.AddRange(
            new Car
            {
                Brand = "BMW",
                Model = "X5",
                Year = 2022,
                LicensePlate = "34 ABC 123",
                DailyPrice = 250,
                IsAvailable = true,
                CreatedDate = DateTime.UtcNow
                // UpdatedDate eklemiyoruz, zaten nullable olduğu için null kalabilir
            },
            new Car
            {
                Brand = "Mercedes",
                Model = "E300",
                Year = 2023,
                LicensePlate = "34 DEF 456",
                DailyPrice = 300,
                IsAvailable = true,
                CreatedDate = DateTime.UtcNow
            },
            new Car
            {
                Brand = "Audi",
                Model = "A6",
                Year = 2021,
                LicensePlate = "34 GHI 789",
                DailyPrice = 280,
                IsAvailable = false,
                CreatedDate = DateTime.UtcNow
            }
        );
        dbContext.SaveChanges();
    }
}

app.Run();