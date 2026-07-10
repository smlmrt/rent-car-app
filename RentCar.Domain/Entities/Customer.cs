using RentCar.Domain.Common;

namespace RentCar.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public string IdentityNumber { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}