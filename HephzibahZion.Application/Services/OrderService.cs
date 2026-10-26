using HephzibahZion.Application.Interfaces;
using HephzibahZion.Domain.Entities;
using HephzibahZion.Domain.Enums;

namespace HephzibahZion.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository; 

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        private void ValidateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.OrderItems == null || !order.OrderItems.Any())
                throw new ArgumentException("An order must have at least one item.");

            if (string.IsNullOrEmpty(order.CustomerName))
                throw new ArgumentException("Customer name is required.");

            if (string.IsNullOrEmpty(order.CustomerPhone))
                throw new ArgumentException("Customer phone is required.");

            if (string.IsNullOrEmpty(order.CustomerAddress))
                throw new ArgumentException("Customer address is required.");

            if (order.OrderItems.Any(i => i.Quantity <= 0))
                throw new ArgumentException("Quantity must be greater than zero.");

        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            ValidateOrder(order);

            var lines = new List<(OrderItem Item, Product Product)>();
            decimal total = 0;

            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);

                if (product == null)
                    throw new ArgumentException($"{product!.ProductName} does not exists.");
                if (!product.IsActive)
                    throw new ArgumentException($"{product.ProductName} is not available");
                if (product.StockQuantity < item.Quantity)
                    throw new ArgumentException($"Not enough stock for {product.ProductName}");

                item.UnitPrice = product.Price;
                total += item.UnitPrice * item.Quantity;
                lines.Add((item, product));
            }

            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            order.TotalAmount = total;

            foreach (var item in order.OrderItems)
            {
                item.Id = Guid.NewGuid();
                item.OrderId = order.Id;
            }

            var savedOrder = await _orderRepository.CreateOrderAsync(order);

            foreach (var (item, product) in lines)
            {
                product.StockQuantity -= item.Quantity;
                await _productRepository.UpdateProductAsync(product);
            }

            return savedOrder;
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

        public async Task<Order?> UpdateOrderStatusAsync(Guid id, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return null;

            if (order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
                throw new InvalidOperationException($"A {order.Status} order can no longer be changed.");

            order.Status = newStatus;
            return await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
   