public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(string id);
    Task AddAsync(Order order);
    Task UpdateAsync(string id, Order order);
    Task DeleteAsync(string id);
}