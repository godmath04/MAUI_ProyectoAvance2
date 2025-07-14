using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Data;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasBDController : ControllerBase
    {
        private readonly RubiaDbContext _context;

        public CategoriasBDController(RubiaDbContext context)
        {
            _context = context;
        }

        // Obtener todas las categorías
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            try
            {
                var categorias = await _context.Categorias.ToListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener categorías", error = ex.Message });
            }
        }

        // Obtener categoría por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
                }

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener categoría", error = ex.Message });
            }
        }

        // Crear nueva categoría
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                categoria.Id = 0;
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear categoría", error = ex.Message });
            }
        }

        // Actualizar categoría
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                {
                    return BadRequest(new { message = "ID no coincide" });
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar categoría", error = ex.Message });
            }
        }

        // Eliminar categoría
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar categoría", error = ex.Message });
            }
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}