public class OrderResponse
{
    public string Id { get; set; } = String.Empty;
    public string OrderNumber { get; set; } = String.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount => OrderItems.Sum(item => item.LineTotal);
    public List<OrderItem> OrderItems { get; set; }
}