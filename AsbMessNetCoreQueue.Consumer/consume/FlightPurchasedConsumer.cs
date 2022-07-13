using AsbMessNetCoreQueue.Contracts;
using MassTransit;

namespace AsbMessNetCoreQueue.Consumer.consume
{
    public class FlightPurchasedConsumer : IConsumer<FlightOrder>
    {
        public Task Consume(ConsumeContext<FlightOrder> context)
        {
            var message = context.Message;
            return Task.CompletedTask;
        }
    }
}
