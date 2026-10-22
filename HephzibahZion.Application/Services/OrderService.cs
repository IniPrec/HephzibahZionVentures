using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;
using HephzibahZion.Domain.Enums;

namespace HephzibahZion.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private async Task ValidatAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await ValidatAsync(order);
            order.Id = Guid.NewGuid();
            return await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetOrdersByStatusAsync(status);
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            await ValidatAsync(order);
            return await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
