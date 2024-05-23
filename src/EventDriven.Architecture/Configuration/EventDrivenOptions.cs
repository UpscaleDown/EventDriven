namespace UpscaleDown.EventDriven.Architecture.Configuration;

public sealed class EventDrivenOptions
{
    public string ORIGIN { get; set; } = string.Empty;

    public string PROVIDER { get; set; } = string.Empty;

    public AuthOptions? AuthOptions { get; set; }
}