using Dapper;
using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;
using HephzibahZion.Domain.Enums;
using HephzibahZion.Infrastructure.Data;

namespace HephzibahZion.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public OrderRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Orders (Id, UserId, CustomerName, CustomerEmail, CustomerPhone, CustomerAddress, OrderDate, Status, TotalAmount) VALUES (@Id, @UserId, @CustomerName, @CustomerEmail, @CustomerPhone, @CustomerAddress, @OrderDate, @Status, @TotalAmount)", order, transaction);

                await connection.ExecuteAsync(
                    "INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice) VALUES (@Id, @OrderId, @ProductId, @Quantity, @UnitPrice)", order.OrderItems, transaction);

                foreach (var item in order.OrderItems)
                {
                    var rows = await connection.ExecuteAsync(
                        "UPDATE Products SET StockQuantity = StockQuantity - @Quantity WHERE Id = @ProductId AND StockQuantity >= @Quantity", item, transaction);

                    if (rows == 0)
                        throw new InvalidOperationException($"Insufficient stock for product {item.ProductId}");
                }

                transaction.Commit();
                return order;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<Order>("SELECT * FROM Orders ORDER BY OrderDate DESC");
        }

        public Task<Order?> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
