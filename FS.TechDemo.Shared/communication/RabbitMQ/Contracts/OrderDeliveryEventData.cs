namespace FS.TechDemo.Shared.communication.RabbitMQ.Contracts;

public record OrderDeliveryEventData
{
    public int Id { get; set; }
    public DateTime DeliveryTime { get; set; }
}