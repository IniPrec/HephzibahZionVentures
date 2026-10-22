using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<Product?> GetProductByIdAsync(Guid id);
        public Task<Product> CreateProductAsync(Product product);
        public Task<Product?> UpdateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(Guid id);
        public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        public Task<IEnumerable<Product>> GetActiveProductsAsync();
        public Task<Product> GetProductByNameAsync();
    }
}
