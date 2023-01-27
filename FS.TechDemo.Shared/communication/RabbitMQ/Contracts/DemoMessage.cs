namespace FS.TechDemo.Shared.communication.RabbitMQ.Contracts;

public record DemoMessage
{
    public string Value { get; set; } = "";
}