using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string product);
    }
}
