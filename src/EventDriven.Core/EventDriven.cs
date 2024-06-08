using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpscaleDown.EventDriven.Architecture.Configuration;
using UpscaleDown.EventDriven.Core.Extensions;
using UpscaleDown.EventDriven.Exceptions;

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
    public static EventDriven Create(string[] args, Action<EventDrivenOptions>? opts = null)
    {
        var builder = WebApplication.CreateBuilder(args);
        var options = new EventDrivenOptions();
        
        if (opts != null) opts(options);
        else options = builder.Configuration.GetSection("EventDrivenOptions").Get<EventDrivenOptions>() ?? 
        throw new OptionsNotFoundException<EventDrivenOptions>(Errors.OptionsErrorCode, Errors.OptionsErrorMessage);

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
    public IConfiguration GetConfiguration(){
        return Builder.Configuration;
}
    public IServiceProvider GetServiceProvider()
    {
        return App.Services;
    }

    public EventDrivenOptions GetOptions(){
        return _options;
    }
}