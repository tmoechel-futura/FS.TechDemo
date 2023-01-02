using FS.TechDemo.OrderService.Entities;

namespace FS.TechDemo.OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private List<Order> orderList = new()
    {
        new Order() { Id = 1, Name = "Order1", Number = "Number1", Total = 1001.11 },
        new Order() { Id = 1, Name = "Order2", Number = "Number2", Total = 1002.22 },
        new Order() { Id = 1, Name = "Order2", Number = "Number3", Total = 1003.33 }
    };
    public List<Order> GetOrderList()
    {
        return orderList;
    }
}