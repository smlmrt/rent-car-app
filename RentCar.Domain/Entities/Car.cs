using RentCar.Domain.Common;

namespace RentCar.Domain.Entities
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = null!;
        public decimal DailyPrice { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl{ get; set; }


        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}