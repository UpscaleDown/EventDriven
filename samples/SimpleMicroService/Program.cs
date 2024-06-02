using UpscaleDown.EventDriven.Samples.SimpleMicroService.Models;
using UpscaleDown.EventDriven.Providers.Extensions;
using UpscaleDown.EventDriven.Core;
using UpscaleDown.EventDriven.Core.Extensions;
using UpscaleDown.EventDriven.Events.Constants;


// First let's setup event driven itself
var eventDriven = EventDriven.Create(args, opts =>
{
    // ORIGIN should be the name of you micro-service
    opts.Origin = "simple1";

    /// depending on your architecture PROVIDER can be usefull or not. 
    /// If you don't know what to do with it, just leave it to 'server'
    opts.Provider = "server";

    /// If you want to you can setup authentication here
    // opts.AuthOptions = new AuthOptions{
    //     SECRET = "Very secret passphrase"
    // };
});

// now let's setup the database provider (in this case MongoDB)
eventDriven.SetupWithMongoDb(opts =>
{
    opts.MONGODB_URI = "mongo://test:pass@host/?retryWrites=true&w=majority";
})
.AddMongoRecordService<SampleRecord>()
.AddMongoNodeService<SampleNode>();

// setup events (in this case rabbitmq)
eventDriven.SetupWithRabbitMQ(opts =>
{
    opts.Host = "rabbitmq";
    opts.EnableRetryOnFailure(1, 2000);
})
// add event publishers
.AddRabbitEventPublisher<SampleRecord>()
.AddRabbitEventPublisher<SampleNode>()
// add event consumers
.AddRabbitEventConsumer<SampleRecord>()
// add event handlers
.AddEventHandler<SampleRecord, SampleRecordAddedHandler>(EventTypes.Added);

eventDriven.Run();
