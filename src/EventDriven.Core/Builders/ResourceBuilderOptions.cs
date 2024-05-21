namespace UpscaleDown.EventDriven.Core.Builders;

public sealed class ResourceBuilderOptions
{
    public string Provider { get; set; } = "";
    public string Origin { get; set; } = "";
    public string Entity { get; set; } = "";

    public ResourceBuilderOptions()
    {

    }
}