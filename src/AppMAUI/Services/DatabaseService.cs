using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MAUI_ProyectoAvance2.Models;

namespace MAUI_ProyectoAvance2.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task Init()
        {
            if (_db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "rubiadivina.db3");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<Producto>();
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            await Init();
            return await _db.Table<Producto>().ToListAsync();
        }

        public async Task<int> AddProductoAsync(Producto producto)
        {
            await Init();
            return await _db.InsertAsync(producto);
        }

        public async Task<int> DeleteProductoAsync(Producto producto)
        {
            await Init();
            return await _db.DeleteAsync(producto);
        }

        public async Task<int> UpdateProductoAsync(Producto producto)
        {
            await Init();
            return await _db.UpdateAsync(producto);
        }
    }
}

