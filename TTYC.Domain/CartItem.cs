namespace TTYC.Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public int Count { get; set; }
        
        public string PriceId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
