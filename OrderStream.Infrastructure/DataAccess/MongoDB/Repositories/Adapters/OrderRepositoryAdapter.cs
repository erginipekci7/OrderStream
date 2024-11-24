using MongoDB.Driver;
using MongoDB.Bson;
using AutoMapper;

public class OrderRepositoryAdapter : IOrderRepository
{
    private readonly IMongoOrderRepository _mongoRepository;
    private readonly IMapper _mapper;

    public OrderRepositoryAdapter(IMongoOrderRepository mongoRepository, IMapper mapper )
    {
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        var mongoOrders = await _mongoRepository.GetAllAsync();

        return _mapper.Map<List<Order>>(mongoOrders);
    }

    public async Task<Order> GetByIdAsync(string id)
    {
        var mongoOrder = await _mongoRepository.GetByIdAsync(id);

        if (mongoOrder == null) return new Order();

        return _mapper.Map<Order>(mongoOrder);
    }

    public async Task AddAsync(Order order)
    {
        var mongoOrder = _mapper.Map<MongoOrder>(order);

        await _mongoRepository.AddAsync(mongoOrder);
    }

    public async Task UpdateAsync(string id, Order order)
    {
        var mongoOrder = _mapper.Map<MongoOrder>(order);

        await _mongoRepository.UpdateAsync(id, mongoOrder);
    }

    public async Task DeleteAsync(string id)
    {
        await _mongoRepository.DeleteAsync(id);
    }
}
