using UpscaleDown.EventDriven.Architecture.Extensions;
using UpscaleDown.EventDriven.Providers.Data.Mongo;
using UpscaleDown.EventDriven.Samples.SimpleMicroService.Models;
using UpscaleDown.EventDriven.Providers.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// First let's setup event driven itself
builder.Services.SetupEventDriven(opts =>
{
    // ORIGIN should be the name of you micro-service
    opts.ORIGIN = "simple1";

    /// depending on your architecture PROVIDER can be usefull or not. 
    /// If you don't know what to do with it, just leave it to 'server'
    opts.PROVIDER = "server";

    /// If you want to you can setup authentication here
    // opts.AuthOptions = new AuthOptions{
    //     SECRET = "Very secret passphrase"
    // };
});

// now let's setup the database provider (in this case MongoDB)
builder.Services.SetupWithMongoDb(opts =>
{
    opts.MONGODB_URI = "mongo://test:pass@host/?retryWrites=true&w=majority";
})
.AddMongoRecordService<SampleRecord>()
.AddMongoNodeService<SampleNode>()
.SetupWithRabbitMQ(opts =>
{
    opts.HOST = "rabbitmq";
    opts.EnableRetryOnFailure(1, 2000);
})
.AddRabbitEventPublisher<SampleRecord>()
.AddRabbitEventPublisher<SampleNode>();

// #TODO: setup event consumers
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
