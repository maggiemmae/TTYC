namespace TTYC.Domain
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
