namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
