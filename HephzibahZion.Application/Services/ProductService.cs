using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Services
{
    public class ProductService : IProductService
    { 
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        private async Task ValidateAsync(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("Product name is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than 0.");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            var category = await _categoryRepository.GetCategoryByIdAsync(product.CategoryId);
            if(category == null)
                throw new ArgumentException("Category does not exist.");
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await ValidateAsync(product);
            product.Id = Guid.NewGuid();
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<bool> DeactivateProductAsync(Guid id)
        {
            return await _productRepository.DeactivateProductAsync(id);
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _productRepository.GetActiveProductsAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
        {
            return await _productRepository.SearchProductsByNameAsync(name);
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            await ValidateAsync(product);
            return await _productRepository.UpdateProductAsync(product);
        }   
    }
}
