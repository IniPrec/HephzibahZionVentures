using HephzibahZion.Domain.Entities;
using HephzibahZion.Domain.Enums;

namespace HephzibahZion.Application.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<Order?> GetOrderByIdAsync(Guid id);
        public Task<Order> CreateOrderAsync(Order order);
        public Task<Order?> UpdateOrderAsync(Order order);
        public Task<bool> DeleteOrderAsync(Guid id);
        public Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
    }
}
