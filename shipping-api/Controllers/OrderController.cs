using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ShippingContext _context;

        public OrderController(ShippingContext context)
        {
            _context = context;
        }

        // Get: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        // Get: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            return order != null ? order : NotFound();
        }
        
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = order.Id}, order);
        }

        // Put: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if(id != order.Id) { return BadRequest(); }

            _context.Entry(order).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if(!Exists(id)) { return NotFound(); }
                throw;
            }

            return NoContent();
        }

        // Delete: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null) { return NotFound(); }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(int id) { return _context.Orders.Any(e => e.Id == id); }

        // dummy method to test the connection
        [HttpGet("hello")]
        public string Test()
        {
            return "Hello World!";
        }
    }
}