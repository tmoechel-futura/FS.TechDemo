using FS.TechDemo.OrderService.Entities;

namespace FS.TechDemo.OrderService.Repositories;

public interface IOrderRepository
{
    List<Order> GetOrderList();
    int AddOrder(string name, string number, double total);
}   