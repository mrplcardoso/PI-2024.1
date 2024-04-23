using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ShippingContext _context;

        public ClientController(ShippingContext context)
        {
            _context = context;
        }

        // Get: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return await _context.Clients.ToListAsync();
        }

        // Get: api/clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            return client != null ? client : NotFound();
        }
        
        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = client.Id}, client);
        }

        // Put: api/clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Client client)
        {
            if(id != client.Id) { return BadRequest(); }

            _context.Entry(client).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if(!Exists(id)) { return NotFound(); }
                throw;
            }

            return NoContent();
        }

        // Delete: api/clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if(client == null) { return NotFound(); }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(int id) { return _context.Clients.Any(e => e.Id == id); }

        // dummy method to test the connection
        [HttpGet("hello")]
        public string Test()
        {
            return "Hello World!";
        }
    }
}