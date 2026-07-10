using RentCar.Domain.Common;

namespace RentCar.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? TransactionId { get; set; }
        public bool IsSuccessful { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}