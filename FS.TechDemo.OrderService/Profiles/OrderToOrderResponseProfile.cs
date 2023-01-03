using AutoMapper;
using FS.TechDemo.OrderService.Entities;
using Shared;

namespace FS.TechDemo.OrderService.Profiles;

public class OrderToOrderResponseProfile : Profile
{
    public OrderToOrderResponseProfile()
    {
        CreateMap<Order, OrderResponse>();
    }
}