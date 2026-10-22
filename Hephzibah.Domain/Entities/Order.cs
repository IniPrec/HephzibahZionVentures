using HephzibahZion.Domain.Enums;

namespace HephzibahZion.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = "";
        public string? CustomerEmail { get; set; }
        public string CustomerPhone { get; set; } = "";
        public string CustomerAddress { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
