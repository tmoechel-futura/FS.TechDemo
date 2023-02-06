using MassTransit.Scheduling;

namespace FS.TechDemo.Shared.communication.RabbitMQ.Schedules;

public class PollExternalSystemSchedule : DefaultRecurringSchedule
{
    public PollExternalSystemSchedule()
    {
        CronExpression = "0 0/1 * 1/1 * ? *"; // this means every minute
    }
}