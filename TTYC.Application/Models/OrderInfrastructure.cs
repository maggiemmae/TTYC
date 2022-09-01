using TTYC.Constants;
using TTYC.Domain;

namespace TTYC.Application.Models
{
    public class OrderInfrastructure
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalSum { get; set; }
        public Address Address { get; set; }
        public IList<CartItem> CartItems { get; set; }
    }
}
