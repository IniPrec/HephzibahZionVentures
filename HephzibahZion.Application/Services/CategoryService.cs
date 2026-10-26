using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        private void ValidateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new ArgumentException("Category name is required.");
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            ValidateCategory(category);
            category.Id = Guid.NewGuid();
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {

            var products = await _productRepository.GetProductsByCategoryIdAsync(id);
            if (products.Any())
                throw new ArgumentException("Cannot delete category with associated products.");

            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            ValidateCategory(category);
            return await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
