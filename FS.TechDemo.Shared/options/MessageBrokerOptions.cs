namespace FS.TechDemo.Shared.options;

public record MessageBrokerOptions
{
    public const string MessageBroker = "MessageBroker";

    public MessageBrokerMetadata Broker { get; init; } = new();
}

public record MessageBrokerMetadata
{
    public DelayedRedelivery DelayedRedelivery { get; init; } = new();

    public MessageRetry MessageRetry { get; init; } = new();

    public RabbitMqOptions RabbitMq { get; init; } = new();
}

public record DelayedRedelivery
{
    public uint FirstIntervalMinutes = 5;
    public uint SecondIntervalMinutes = 10;
    public uint ThirdIntervalMinutes = 20;
}

public record MessageRetry
{
    public uint Limit = 3;
    public uint InitialIntervalMilliseconds = 500;
    public uint IntervalIncrementMilliseconds = 500;
}

public record RabbitMqOptions
{
    public string Host { get; set; } = "";
    public string VirtualHost { get; set; } = "";

    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
