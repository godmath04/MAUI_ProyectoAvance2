using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Data;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesBDController : ControllerBase
    {
        private readonly RubiaDbContext _context;

        public ClientesBDController(RubiaDbContext context)
        {
            _context = context;
        }

        //Obtener todos los clientes 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _context.Cliente.ToListAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener clientes", error = ex.Message });
            }
        }

        //Obtener cliente por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);

                if (cliente == null)
                {
                    return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener cliente", error = ex.Message });
            }
        }

        //Actualizar cliente por id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                {
                    return BadRequest(new { message = "ID no coincide" });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Entry(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar cliente", error = ex.Message });
            }
        }

        //Crear cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                cliente.Id = 0;
                cliente.FechaCreacion = DateTime.UtcNow;
                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear cliente", error = ex.Message });
            }
        }

        //Borrar cliente por id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
                }

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar cliente", error = ex.Message });
            }
        }

        // Buscar cliente por correo
        [HttpGet("buscar-email/{email}")]
        public async Task<ActionResult<Cliente>> GetClientePorEmail(string email)
        {
            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.Correo == email);

                if (cliente == null)
                {
                    return NotFound(new { message = "Cliente no encontrado" });
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar cliente", error = ex.Message });
            }
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}