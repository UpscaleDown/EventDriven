using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Core.Extensions;

namespace UpscaleDown.EventDriven.Core;

public class EventDriven
{
    private readonly EventDrivenOptions _options;
    internal readonly WebApplicationBuilder Builder;
    public IServiceCollection Services => Builder.Services;
    internal WebApplication App { get; private set; }
    public EventDriven(EventDrivenOptions options, WebApplicationBuilder builder)
    {
        _options = options;
        Builder = builder;
    }
    public static EventDriven Create(string[] args, Action<EventDrivenOptions> opts)
    {
        var builder = WebApplication.CreateBuilder(args);
        var options = new EventDrivenOptions();
        opts(options);
        builder.Services.AddSingleton(options);
        var ev = new EventDriven(options, builder);
        ev.AddServices(options);
        builder.Services.AddSingleton(ev);
        return ev;
    }

    public WebApplication Build()
    {
        App = Builder.Build();
        return App;
    }

    public IServiceProvider GetServiceProvider()
    {
        return App.Services;
    }
}