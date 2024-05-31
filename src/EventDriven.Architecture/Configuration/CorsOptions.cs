namespace UpscaleDown.EventDriven.Architecture.Configuration;

public sealed class CorsOptions
{
    public List<string> AllowedOrigins { get; set; } = new List<string>();
}