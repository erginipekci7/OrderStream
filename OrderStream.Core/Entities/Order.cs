public class Order
{
    public string Id { get; set; } = string.Empty; // MongoRepository'de otomatik olarak oluşturulur
    public int CustomerId { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
}