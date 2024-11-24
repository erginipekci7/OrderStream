namespace OrderStream.Application.Services;
using OrderStream.Application.Exceptions;
using Serilog;


public class OrderService(IOrderRepository orderRepository, ILogger logger) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        _logger.Information("Retrieving all orders");
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(string id)
    {
        _logger.Information("Retrieving order with ID: {OrderId}", id);
        
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));

        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            _logger.Warning("Order not found with ID: {OrderId}", id);
            throw new OrderNotFoundException($"Order with ID {id} was not found");
        }

        return order;
    }

    public async Task AddOrderAsync(Order order)
    {
        _logger.Information("Adding new order with number: {OrderNumber}", order.OrderNumber);
        ValidateOrder(order);

        try
        {
            await _orderRepository.AddAsync(order);
            _logger.Information("Successfully added order: {OrderNumber}", order.OrderNumber);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while creating order {OrderNumber}", order.OrderNumber);
            throw;
        }
    }

    public async Task UpdateOrderAsync(string id, Order order)
    {
        _logger.Information("Updating order with ID: {OrderId}", id);
        
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));

        ArgumentNullException.ThrowIfNull(order);

        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder is null)
        {
            _logger.Warning("Order not found for update with ID: {OrderId}", id);
            throw new OrderNotFoundException($"Order with ID {id} was not found");
        }

        await _orderRepository.UpdateAsync(id, order);
        _logger.Information("Successfully updated order: {OrderId}", id);
    }

    public async Task DeleteOrderAsync(string id)
    {
        _logger.Information("Deleting order with ID: {OrderId}", id);
        
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));

        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
        {
            _logger.Warning("Order not found for deletion with ID: {OrderId}", id);
            throw new OrderNotFoundException($"Order with ID {id} was not found");
        }

        await _orderRepository.DeleteAsync(id);
        _logger.Information("Successfully deleted order: {OrderId}", id);
    }

    private static void ValidateOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (string.IsNullOrEmpty(order.OrderNumber))
            throw new OrderValidationException("Order Number is required");
    }
}
