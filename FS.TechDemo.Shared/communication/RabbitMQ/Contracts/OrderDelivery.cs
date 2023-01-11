namespace FS.TechDemo.Shared.communication.RabbitMQ.Contracts;

public record OrderDelivery : IMessageContract
{
    public string OrderName { get; set; } = "";
    public int OrderSum { get; set; }
}