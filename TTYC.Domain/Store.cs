namespace TTYC.Domain
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
