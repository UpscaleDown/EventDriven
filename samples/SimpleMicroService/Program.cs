using UpscaleDown.EventDriven.Samples.SimpleMicroService.Models;
using UpscaleDown.EventDriven.Providers.Extensions;
using UpscaleDown.EventDriven.Core;
using UpscaleDown.EventDriven.Core.Extensions;


// First let's setup event driven itself
// If you don't provide opts here event driven will try to pull the options from the configuration
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
eventDriven.SetupWithMongoDb(eventDriven.GetMongoOptions())
.AddMongoRecordService<SampleRecord>()
.AddMongoNodeService<SampleNode>()
.SetupWithRabbitMQ(opts =>
{
    opts.HOST = "rabbitmq";
    opts.EnableRetryOnFailure(1, 2000);
})
.AddRabbitEventPublisher<SampleRecord>()
.AddRabbitEventPublisher<SampleNode>();

eventDriven.Run();
