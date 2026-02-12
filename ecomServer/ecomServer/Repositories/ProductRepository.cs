using ecomServer.Data;
using ecomServer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ecomServer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcomDbContext _db;
        public ProductRepository(EcomDbContext db)
        {
            _db = db;
        }

        public async Task<Models.Product> AddProductAsync(Models.Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return product;
        }
        public async Task<Models.Product> UpdateProductAsync(Models.Product product)
        {

                  _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProductAsync(int ProductId)
        {
            var product = await _db.Products.FindAsync(ProductId);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Models.Product>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Models.Product?> GetProductByIdAsync(int ProductId)
        {
            var product = await _db.Products.FindAsync(ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {ProductId} not found.");
            }
            return product;
        }
    }
}