using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Order.Include(o => o.Customer).Include(o => o.OrderItem).ThenInclude(oi => oi.Product).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // Müşterinin veritabanında olup olmadığını kontrol et
            
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == order.CustomerId);
            if (!customerExists)
            {
                return BadRequest("Müşteri bulunamadı.");
            }

            // Siparişin durumu ve tarihini atayalım
            order.Status = "Pending"; // veya siparişin durumuna göre farklı bir değer atayabilirsiniz.
            order.OrderDate = DateTime.UtcNow;

            // Order nesnesini veritabanına ekleyin ve kaydedin
            _context.Order.Add(order);

            // OrderItem'ları da ekleyelim (OrderItem verisinin Order nesnesine ilişkilendirildiğinden emin olun)
            if (order.OrderItem != null && order.OrderItem.Any())
            {
                foreach (var orderItem in order.OrderItem)
                {
                    // OrderItem'ları veritabanına eklerken doğru ilişkileri sağlamak önemli
                    orderItem.OrderId = order.Id; // OrderId, eklenen order'ın Id'sini alacak
                    _context.OrderItem.Add(orderItem);
                }
            }

            // Veritabanındaki değişiklikleri kaydedin
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın veya döndürün
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            // Yeni oluşturulan Order'ı geri döndürelim
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }


        // PUT: api/orders/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutOrderStatus(int id, string status)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.Status != "Pending")
            {
                return BadRequest("Sadece 'Pending' durumundaki siparişler güncellenebilir.");
            }

            order.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/customers/{id}/orders
        [HttpGet("customers/{id}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(int id)
        {
            return await _context.Order.Where(o => o.CustomerId == id).Include(o => o.OrderItem).ThenInclude(oi => oi.Product).ToListAsync();
        }

        // GET: api/orders/{id}/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems(int id)
        {
            return await _context.OrderItem.Where(oi => oi.OrderId == id).Include(oi => oi.Product).ToListAsync();
        }
    }

}
