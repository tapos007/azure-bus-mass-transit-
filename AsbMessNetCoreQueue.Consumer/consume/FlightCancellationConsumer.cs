using AsbMessNetCoreQueue.Contracts;
using MassTransit;

namespace AsbMessNetCoreQueue.Consumer.consume
{
    public class FlightCancellationConsumer : IConsumer<FlightCancellation>
    {
        public Task Consume(ConsumeContext<FlightCancellation> context)
        {
            var message = context.Message;
            return Task.CompletedTask;
        }
    }
}
