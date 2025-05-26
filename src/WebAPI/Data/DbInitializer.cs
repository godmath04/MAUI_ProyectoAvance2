using RubiaDivinaWebAPI.Models;

namespace RubiaDivinaWebAPI.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RubiaDbContext>();

            // Agregar Categorías si no existen
            if (!context.Categorias.Any())
            {
                var categorias = new List<Categoria>
            {
                new() { Nombre = "Bebidas" },
                new() { Nombre = "Postres" },
                new() { Nombre = "Picadas" },
                new() { Nombre = "Cervezas" }
            };
                context.Categorias.AddRange(categorias);
                context.SaveChanges();
            }

            // Agregar Productos nuevos (solo si no existen muchachos)
            if (!context.Productos.Any())
            {
                var productos = new List<Producto>
            {
                new() { Nombre = "Café Americano", Precio = 2.50m, Stock = 50, CategoriaId = 1 },
                new() { Nombre = "Capuchino", Precio = 3.00m, Stock = 40, CategoriaId = 1 },
                new() { Nombre = "Latte", Precio = 3.20m, Stock = 35, CategoriaId = 1 },
                new() { Nombre = "Espresso", Precio = 2.00m, Stock = 60, CategoriaId = 1 },
                new() { Nombre = "Mocaccino", Precio = 3.50m, Stock = 30, CategoriaId = 1 },

                new() { Nombre = "Pastel de Chocolate", Precio = 4.50m, Stock = 20, CategoriaId = 2 },
                new() { Nombre = "Tarta de Frutilla", Precio = 4.00m, Stock = 20, CategoriaId = 2 },
                new() { Nombre = "Cheesecake", Precio = 4.80m, Stock = 15, CategoriaId = 2 },
                new() { Nombre = "Galletas de Avena", Precio = 1.50m, Stock = 50, CategoriaId = 2 },
                new() { Nombre = "Brownie", Precio = 2.80m, Stock = 25, CategoriaId = 2 },

                new() { Nombre = "Nachos con Guacamole", Precio = 5.50m, Stock = 15, CategoriaId = 3 },
                new() { Nombre = "Tabla de Quesos y Jamones", Precio = 12.00m, Stock = 10, CategoriaId = 3 },
                new() { Nombre = "Papas Bravas", Precio = 4.00m, Stock = 20, CategoriaId = 3 },
                new() { Nombre = "Alitas BBQ", Precio = 6.00m, Stock = 15, CategoriaId = 3 },

                new() { Nombre = "Rubia Divina", Precio = 3.50m, Stock = 100, CategoriaId = 4 },
                new() { Nombre = "Negra Divina", Precio = 3.80m, Stock = 100, CategoriaId = 4 },
                new() { Nombre = "Roja Divina", Precio = 3.70m, Stock = 100, CategoriaId = 4 },
                new() { Nombre = "Amarilla Divina", Precio = 3.60m, Stock = 100, CategoriaId = 4 }
            };

                context.Productos.AddRange(productos);
                context.SaveChanges();
            }
        }
    }
}
