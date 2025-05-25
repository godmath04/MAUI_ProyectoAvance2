using Microsoft.EntityFrameworkCore;
using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Data
{
    public class RubiaDbContext : DbContext
    {
        public RubiaDbContext(DbContextOptions<RubiaDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}



