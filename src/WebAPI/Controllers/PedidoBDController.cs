using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Data;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoBDController : ControllerBase
    {
        private readonly RubiaDbContext _context;

        public PedidoBDController(RubiaDbContext context)
        {
            _context = context;
        }

        // Obtener todos los pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            try
            {
                var pedidos = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Producto)
                    .ToListAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener pedidos", error = ex.Message });
            }
        }

        // Obtener pedido por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Producto)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pedido == null)
                {
                    return NotFound(new { message = $"Pedido con ID {id} no encontrado" });
                }

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener pedido", error = ex.Message });
            }
        }

        // Crear nuevo pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                pedido.Id = 0;
                pedido.FechaSolicitud = DateTime.UtcNow;
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                // Cargar las relaciones para la respuesta
                await _context.Entry(pedido)
                    .Reference(p => p.Cliente)
                    .LoadAsync();
                await _context.Entry(pedido)
                    .Reference(p => p.Producto)
                    .LoadAsync();

                return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear pedido", error = ex.Message });
            }
        }

        // Actualizar pedido
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            try
            {
                if (id != pedido.Id)
                {
                    return BadRequest(new { message = "ID no coincide" });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Entry(pedido).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound(new { message = $"Pedido con ID {id} no encontrado" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar pedido", error = ex.Message });
            }
        }

        // Eliminar pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    return NotFound(new { message = $"Pedido con ID {id} no encontrado" });
                }

                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar pedido", error = ex.Message });
            }
        }

        // Obtener pedidos por cliente
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidosPorCliente(int clienteId)
        {
            try
            {
                var pedidos = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Producto)
                    .Where(p => p.ClienteId == clienteId)
                    .ToListAsync();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener pedidos por cliente", error = ex.Message });
            }
        }

        // Marcar pedido como pagado
        [HttpPatch("{id}/pagar")]
        public async Task<IActionResult> MarcarComoPagado(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);
                if (pedido == null)
                {
                    return NotFound(new { message = $"Pedido con ID {id} no encontrado" });
                }

                pedido.Pagado = true;
                pedido.FechaPago = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al marcar pedido como pagado", error = ex.Message });
            }
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}