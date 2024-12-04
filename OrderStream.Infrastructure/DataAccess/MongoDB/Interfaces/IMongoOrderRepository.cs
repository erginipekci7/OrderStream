public interface IMongoOrderRepository
{
    Task<List<MongoOrder>> GetAllAsync();
    Task<MongoOrder> GetByIdAsync(string id);
    Task AddAsync(MongoOrder order);
    Task UpdateAsync(string id, MongoOrder order);
    Task<bool> DeleteAsync(string id);
}
