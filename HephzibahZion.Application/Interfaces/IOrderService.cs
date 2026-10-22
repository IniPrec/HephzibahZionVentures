using HephzibahZion.Domain.Entities;
using HephzibahZion.Domain.Enums;

namespace HephzibahZion.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
    }
}
