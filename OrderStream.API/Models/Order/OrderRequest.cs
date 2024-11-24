public class OrderRequest
{
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; } = 1;
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}