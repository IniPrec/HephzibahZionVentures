using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<Category?> GetCategoryByIdAsync(Guid id);
        public Task<Category> CreateCategoryAsync(Category category);
        public Task<Category?> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(Guid id);
    }
}
