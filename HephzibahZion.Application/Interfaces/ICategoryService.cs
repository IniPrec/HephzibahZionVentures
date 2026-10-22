using HephzibahZion.Domain.Entities;

namespace HephzibahZion.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
