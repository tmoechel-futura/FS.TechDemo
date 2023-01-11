using MassTransit;
using Microsoft.Extensions.Logging;

namespace FS.TechDemo.Shared.communication.RabbitMQ.Consumer;

public class GenericFaultConsumer<T> : IConsumer<Fault<T>>
{
    protected readonly ILogger _logger;
    public GenericFaultConsumer(ILogger logger) => _logger = logger;
    
    public virtual Task Consume(ConsumeContext<Fault<T>> context)
    {
        _logger.LogInformation("Fault Timestamp: {Param}", context.Message.Timestamp);
        _logger.LogInformation("Fault MachineName: {Param}", context.Message.Host.MachineName);
        _logger.LogInformation("Fault Host: {Param}", context.Message.Host.MachineName);
        _logger.LogInformation("Fault MessageId: {Param}", context.Message.FaultedMessageId);
        foreach (var messageException in context.Message.Exceptions) {
            _logger.LogInformation("Exception Message: {Param}", messageException.Message);
            _logger.LogInformation("Exception Type: {Param}", messageException.ExceptionType);
            _logger.LogInformation("Exception Source: {Param}", messageException.Source);
            _logger.LogInformation("Exception InnerException: {Param}", messageException.InnerException);
        }
        return Task.CompletedTask;
    }
}