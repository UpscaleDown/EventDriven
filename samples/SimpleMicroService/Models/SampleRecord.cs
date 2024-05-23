using UpscaleDown.EventDriven.Core.Models;

namespace UpscaleDown.EventDriven.Samples.SimpleMicroService.Models;

public class SampleRecord : Record
{
    public string? ZIPCode { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
}