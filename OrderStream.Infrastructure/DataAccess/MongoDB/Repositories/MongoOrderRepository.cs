using MongoDB.Driver;
using MongoDB.Bson;

public class MongoOrderRepository : IMongoOrderRepository
{
    private readonly IMongoCollection<MongoOrder> _orderCollection;

    public MongoOrderRepository(MongoDbContext context)
    {
        _orderCollection = context.Orders;
    }
    public async Task<List<MongoOrder>> GetAllAsync()
    {
        return await _orderCollection.Find(_ => true).ToListAsync();
    }

    public async Task<MongoOrder> GetByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        return await _orderCollection.Find(order => order.Id == objectId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(MongoOrder order)
    {
        await _orderCollection.InsertOneAsync(order);
    }

    public async Task UpdateAsync(string id, MongoOrder order)
    {
        var objectId = ObjectId.Parse(id);

        // Ensure the order's Id is not altered
        order.Id = objectId;

        var filter = Builders<MongoOrder>.Filter.Eq(o => o.Id, objectId);
        await _orderCollection.ReplaceOneAsync(filter, order);
    }

    public async Task DeleteAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        await _orderCollection.DeleteOneAsync(order => order.Id == objectId);
    }
}