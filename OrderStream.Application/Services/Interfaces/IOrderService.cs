public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(string id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(string id, Order order);
    Task DeleteOrderAsync(string id);
}
