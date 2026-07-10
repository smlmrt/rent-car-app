namespace RentCar.Shared.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;  // ✅ Plaka da olsun
        public decimal DailyPrice { get; set; }  // ✅ DailyPrice
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class CreateCarDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public decimal DailyPrice { get; set; }
    }
}