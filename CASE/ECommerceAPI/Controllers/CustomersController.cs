using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            // Yeni müşteriyi ekliyoruz
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Yeni eklenen müşteri ile birlikte "Created" HTTP durumu döndürüyoruz
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }
    }

}
