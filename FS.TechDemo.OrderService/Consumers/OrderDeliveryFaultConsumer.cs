using FS.TechDemo.Shared.communication.RabbitMQ.Consumer;
using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
using MassTransit;

namespace FS.TechDemo.OrderService.Consumers;

public class OrderDeliveryFaultConsumer : GenericFaultConsumer<OrderDelivery>

{
    public OrderDeliveryFaultConsumer(ILogger<OrderDeliveryFaultConsumer> logger) : base(logger)
    {}

    public override Task Consume(ConsumeContext<Fault<OrderDelivery>> context)
    {
        _logger.LogInformation("Fault Message: {Param}", context.Message.Message);
        return base.Consume(context);
    }
}