using MongoDB.Driver;
using Product.API.Context;
using Product.API.Interfaces.Repository;

namespace Product.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Models.Product> _products;

        public ProductRepository(ProductDbContext context)
        {
            _products = context.Products;
        }

        public async Task CreateProduct(Models.Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _products.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Models.Product> GetProduct(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Models.Product>> GetProducts()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(string id, Models.Product product)
        {
            var result = _products.ReplaceOne(p => p.Id == id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
