namespace UpscaleDown.EventDriven.Architecture.Configuration;

public sealed class EventDrivenOptions
{
    public string Origin { get; set; } = string.Empty;

    public string Provider { get; set; } = string.Empty;

    public AuthOptions? AuthOptions { get; set; }

    public CorsOptions? CorsOptions { get; set; }
}