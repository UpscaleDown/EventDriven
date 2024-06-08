using UpscaleDown.EventDriven.Events;
using UpscaleDown.EventDriven.Events.Interfaces;
using UpscaleDown.EventDriven.Samples.SimpleMicroService.Models;

public class SampleRecordAddedHandler : IEventHandler<SampleRecord>
{
    public async Task HandleAsync(SampleRecord data, Event @event)
    {
        // do something
    }

    public async Task HandleAsync(List<SampleRecord> data, Event @event)
    {
        // do something
    }
}