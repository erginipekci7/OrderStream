using AutoMapper;

namespace OrderStream.API.Mapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderRequest, Order>();
    }
}