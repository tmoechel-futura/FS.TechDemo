using FS.TechDemo.Shared.communication.RabbitMQ.Contracts;
// using MassTransit;

namespace FS.TechDemo.OrderService.Consumers;

// public class OrderDeliveryFaultConsumer : IConsumer<Fault<OrderDelivery>>
//
// {
//     private readonly ILogger<OrderDeliveryFaultConsumer> _logger;
//
//     public OrderDeliveryFaultConsumer(ILogger<OrderDeliveryFaultConsumer> logger)
//     {
//         _logger = logger;
//     }
//     
//     public Task Consume(ConsumeContext<Fault<OrderDelivery>> context)
//     {
//         _logger.LogInformation("Fault Order message: {Param}", context.Message.Message);
//         _logger.LogInformation("Fault Timestampg: {Param}", context.Message.Timestamp);
//         _logger.LogInformation("Fault Host: {Param}", context.Message.Host.MachineName);
//         _logger.LogInformation("Fault MessageId: {Param}", context.Message.FaultedMessageId);
//         
//         return Task.CompletedTask;
//     }
// }