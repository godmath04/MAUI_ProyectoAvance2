using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Data;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosBDController : ControllerBase
    {
        private readonly RubiaDbContext _context;

        public ProductosBDController(RubiaDbContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los productos: {ex.Message}");
            }
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.Include(p => p.Categoria)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (producto == null)
                {
                    return NotFound(new { Message = $"Producto con {id} no encontrado" });
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el producto con {id}: {ex.Message}");
            }
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                //Cargar la categoría para la respuesta
                await _context.Entry(producto)
                    .Reference(p => p.Categoria)
                    .LoadAsync();

                return CreatedAtAction("GetProducto", new {id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500, 
                    new { message = "Error al crear producto", error = ex.Message });
            }
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest(new { message = $"ID {id} no coincide" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound(new { message = $"Producto con ID {id} no encontrado" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar producto", error = ex.Message });
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        // Eliminar producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound(new { message = $"Producto con ID {id} no encontrado" });
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar producto", error = ex.Message });
            }
        }

        // Obtener productos por categoría
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosPorCategoria(int categoriaId)
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Categoria)
                    .Where(p => p.CategoriaId == categoriaId)
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener productos por categoría", error = ex.Message });
            }
        }

        // Buscar productos por nombre
        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<IEnumerable<Producto>>> BuscarProductos(string termino)
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Categoria)
                    .Where(p => p.Nombre.Contains(termino))
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar productos", error = ex.Message });
            }
        }
    }
}
