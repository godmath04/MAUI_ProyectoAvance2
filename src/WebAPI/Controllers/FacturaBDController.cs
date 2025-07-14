using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Data;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaBDController : ControllerBase
    {
        private readonly RubiaDbContext _context;

        public FacturaBDController(RubiaDbContext context)
        {
            _context = context;
        }

        // Obtener todas las facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            try
            {
                var facturas = await _context.Facturas
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Cliente)
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Producto)
                    .ToListAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener facturas", error = ex.Message });
            }
        }

        // Obtener factura por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Cliente)
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Producto)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (factura == null)
                {
                    return NotFound(new { message = $"Factura con ID {id} no encontrada" });
                }

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener factura", error = ex.Message });
            }
        }

        // Crear nueva factura
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                factura.Id = 0;
                factura.FechaFactura = DateTime.UtcNow;
                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();

                // Cargar las relaciones para la respuesta
                await _context.Entry(factura)
                    .Reference(f => f.Pedido)
                    .LoadAsync();

                return CreatedAtAction("GetFactura", new { id = factura.Id }, factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear factura", error = ex.Message });
            }
        }

        // Actualizar factura
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            try
            {
                if (id != factura.Id)
                {
                    return BadRequest(new { message = "ID no coincide" });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Entry(factura).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
                {
                    return NotFound(new { message = $"Factura con ID {id} no encontrada" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar factura", error = ex.Message });
            }
        }

        // Eliminar factura
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas.FindAsync(id);
                if (factura == null)
                {
                    return NotFound(new { message = $"Factura con ID {id} no encontrada" });
                }

                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar factura", error = ex.Message });
            }
        }

        // Obtener facturas por pedido
        [HttpGet("pedido/{pedidoId}")]
        public async Task<ActionResult<Factura>> GetFacturaPorPedido(int pedidoId)
        {
            try
            {
                var factura = await _context.Facturas
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Cliente)
                    .Include(f => f.Pedido)
                        .ThenInclude(p => p.Producto)
                    .FirstOrDefaultAsync(f => f.PedidoId == pedidoId);

                if (factura == null)
                {
                    return NotFound(new { message = "Factura no encontrada para este pedido" });
                }

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener factura por pedido", error = ex.Message });
            }
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}