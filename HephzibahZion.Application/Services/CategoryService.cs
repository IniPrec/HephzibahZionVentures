using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private async Task ValidateAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new ArgumentException("Category name is required.");
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await ValidateAsync(category);
            category.Id = Guid.NewGuid();
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
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
            await ValidateAsync(category);
            return await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
