using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }  // Siparişin ID'si, bu şekilde JSON'da kullanılacak
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigasyon özellikleri (veritabanı ilişkisi için kullanılır)
        //public string Order { get; set; }
        public Product Product { get; set; }
    }


}
