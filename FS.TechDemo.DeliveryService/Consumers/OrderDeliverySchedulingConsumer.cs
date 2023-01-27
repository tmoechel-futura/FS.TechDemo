using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
using MassTransit;

namespace DeliveryService.Consumers;

public class OrderDeliverySchedulingConsumer : IConsumer<OrderDeliveryEventData>
{
    private readonly ILogger<OrderDeliverySchedulingConsumer> _logger;

    public OrderDeliverySchedulingConsumer(ILogger<OrderDeliverySchedulingConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<OrderDeliveryEventData> context)
    {
        if (string.IsNullOrEmpty(Convert.ToString(context.Message.Id))) throw new ArgumentException("Order id must not be empty");
        _logger.LogInformation("DeliveryTime of the order {DeliveryTime}", context.Message.DeliveryTime);

        return Task.CompletedTask;
    }
    
}