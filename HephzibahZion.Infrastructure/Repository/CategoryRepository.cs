using Dapper;
using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;
using HephzibahZion.Infrastructure.Data;

namespace HephzibahZion.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public CategoryRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
   
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            await connection.ExecuteAsync("INSERT INTO Categories(Id, CategoryName) VALUES (@Id, @CategoryName)", category);

            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
               
            var rows = await connection.ExecuteAsync("DELETE FROM Categories WHERE Id = @Id", new { Id = id });

            return rows > 0;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<Category>(
                "SELECT Id, CategoryName FROM Categories");
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Category>("SELECT Id, CategoryName FROM Categories WHERE Id = @Id", new { Id = id });
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var rows = await connection.ExecuteAsync("UPDATE Categories SET CategoryName = @CategoryName WHERE Id = @Id", category);

            return rows > 0 ? category : null;
        }
    }
}
