using Dapper;
using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;
using HephzibahZion.Infrastructure.Data;

namespace HephzibahZion.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ProductRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "INSERT INTO Products (Id, ProductName, Price, StockQuantity, ImageUrl, CategoryId, IsActive) " +
                "VALUES (@Id, @ProductName, @Price, @StockQuantity, @ImageUrl, @CategoryId, @IsActive)",
                product);

            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();

            var rows = await connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });

            return rows > 0;
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Product>("SELECT * FROM Products WHERE IsActive = 1");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Product>("SELECT * FROM Products WHERE CategoryId = @CategoryId", new { CategoryId = categoryId });
        }

        public Task<IEnumerable<Product>> SearchProductsByNameAsync(string product)
        {
            using var connection = _connectionFactory.CreateConnection();
            
            return connection.QueryAsync<Product>("SELECT * FROM Products WHERE ProductName LIKE @ProductName", new { ProductName = $"%{product}%" });
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();

            var column = await connection.ExecuteAsync("UPDATE Products SET ProductName = @ProductName WHERE Id = @Id", product);

            return column > 0 ? product : null;
        }
    }
}
