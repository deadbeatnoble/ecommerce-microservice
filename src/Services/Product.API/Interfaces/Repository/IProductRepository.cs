namespace Product.API.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Models.Product>> GetProducts();
        Task<Models.Product> GetProduct(string id);
        public Task CreateProduct(Models.Product product);
        Task<bool> UpdateProduct(string id, Models.Product product);
        Task<bool> DeleteProduct(string id);
    }
}
