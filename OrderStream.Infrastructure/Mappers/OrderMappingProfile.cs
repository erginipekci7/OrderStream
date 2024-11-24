using AutoMapper;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<MongoOrder, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

        CreateMap<MongoOrderItem, OrderItem>();

        CreateMap<Order, MongoOrder>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id genellikle MongoDB tarafından atanır
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, MongoOrderItem>();
    }
}