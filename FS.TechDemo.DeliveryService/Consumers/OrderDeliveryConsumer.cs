using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
using MassTransit;

namespace DeliveryService.Consumers;

public class OrderDeliveryConsumer : IConsumer<OrderDelivery>

{
    private readonly ILogger<OrderDeliveryConsumer> _logger;

    public OrderDeliveryConsumer(ILogger<OrderDeliveryConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<OrderDelivery> context)
    {
        _logger.LogInformation("Entering consumer");
        if (string.IsNullOrEmpty(context.Message.OrderName)) throw new ArgumentException("Order name must not be empty");
        if (context.Message.OrderName.Length > 10) throw new ConsumerException("Order name exceeds 10 characters");
        _logger.LogInformation("Thank you for ordering: {Order}", context.Message.OrderName);
        return Task.CompletedTask;
    }
}