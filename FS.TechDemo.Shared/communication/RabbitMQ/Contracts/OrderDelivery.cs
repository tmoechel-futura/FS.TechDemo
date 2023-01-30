namespace FS.TechDemo.Shared.communication.RabbitMQ.Contracts;

public record OrderDelivery
{
    public string OrderName { get; set; } = "";
}