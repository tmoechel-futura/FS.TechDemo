using FS.TechDemo.OrderService.Entities;

namespace FS.TechDemo.OrderService.Repositories;

public interface IOrderRepository
{
    List<Order> GetOrderList();
}