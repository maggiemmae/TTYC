using System.Globalization;

namespace TTYC.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceId { get; set; }
        public string StripeId { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<Store> Stores { get; set; }
    }
}
