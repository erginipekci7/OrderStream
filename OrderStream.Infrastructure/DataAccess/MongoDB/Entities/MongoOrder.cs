using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoOrder
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string OrderNumber { get; set; } = String.Empty;
    public DateTime OrderDate { get; set; }
    [BsonElement("CustomerId")]
    public int CustomerId { get; set; } // PostgreSQL'deki Customer tablosuna referans
    public List<MongoOrderItem> Items { get; set; } = new();
}
