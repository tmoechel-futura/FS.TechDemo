namespace FS.TechDemo.OrderService.Entities;

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Number { get; set; } = "";
    public double Total { get; set; }
}