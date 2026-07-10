using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Infrastructure.Persistence;
using RentCar.Shared.DTOs;

namespace RentCar.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/cars
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _context.Cars
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    LicensePlate = c.LicensePlate,  // ✅ Plaka eklendi
                    DailyPrice = c.DailyPrice,
                    IsAvailable = c.IsAvailable,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            var carDto = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                LicensePlate = car.LicensePlate,
                DailyPrice = car.DailyPrice,
                IsAvailable = car.IsAvailable,
                ImageUrl = car.ImageUrl
            };

            return Ok(carDto);
        }

        // POST: api/v1/cars
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarDto createCarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = new Car
            {
                Brand = createCarDto.Brand,
                Model = createCarDto.Model,
                Year = createCarDto.Year,
                LicensePlate = createCarDto.LicensePlate,
                DailyPrice = createCarDto.DailyPrice,
                IsAvailable = true,
                CreatedDate = DateTime.UtcNow
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            var carDto = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                DailyPrice = car.DailyPrice,
                IsAvailable = car.IsAvailable,
                ImageUrl = car.ImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, carDto);
        }

        // POST: api/v1/cars/{id}/rent
        [HttpPost("{id}/rent")]
        public async Task<IActionResult> Rent(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            if (!car.IsAvailable)
                return BadRequest("Bu araç zaten kirada.");

            car.IsAvailable = false;
            // UpdatedDate kaldırıldı (BaseEntity'de var ama zorunlu değil)
            await _context.SaveChangesAsync();

            return Ok(new { message = "Araç başarıyla kiralandı.", carId = id });
        }

        // PUT: api/v1/cars/{id}/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            if (car.IsAvailable)
                return BadRequest("Bu araç zaten müsait.");

            car.IsAvailable = true;
            // UpdatedDate kaldırıldı
            await _context.SaveChangesAsync();

            return Ok(new { message = "Araç başarıyla teslim alındı.", carId = id });
        }

        // DELETE: api/v1/cars/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}