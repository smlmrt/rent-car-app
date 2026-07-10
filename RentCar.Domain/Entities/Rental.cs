using RentCar.Domain.Common;

namespace RentCar.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCompleted { get; set; }

        public Car Car { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public Payment? Payment { get; set; }
    }
}