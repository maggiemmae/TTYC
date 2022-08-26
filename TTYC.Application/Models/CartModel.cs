using TTYC.Domain;

namespace TTYC.Application.Models
{
    public class CartModel
    {
        public IList<CartItem> CartItems { get; set; }
        public decimal TotalSum { get; set; }
    }
}
