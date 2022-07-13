
using AsbMessNetCoreQueue.Consumer.consume;
using AsbMessNetCoreQueue.Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
   
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("ServiceBus"));

        cfg.Message<FlightOrder>(configTopology =>
        {
            configTopology.SetEntityName(typeof(FlightOrder).FullName);
        });
        // cfg.SubscriptionEndpoint(
        //     "tapos_plan",
        //     "shape",
        //     e =>
        //     {
        //         e.Handler<Shape>(async context =>
        //         {
        //             await Console.Out.WriteLineAsync($"Shape Received: {context.Message.Type}");
        //         });
        //
        //         e.MaxDeliveryCount = 15;
        //     });





        // setup Azure topic consumer
        cfg.SubscriptionEndpoint<FlightOrder>("tapos_plan", configurator =>
        {
            configurator.Consumer<FlightPurchasedConsumer>();
            
        });


        // setup Azure queue consumer
        cfg.ReceiveEndpoint(typeof(FlightCancellation).FullName.ToLower(), configurator =>
        {
            configurator.Consumer<FlightCancellationConsumer>();
        });

    });
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
