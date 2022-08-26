using TTYC.Constants;

namespace TTYC.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalSum { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual IList<CartItem> CartItems { get; set; }
    }
}
