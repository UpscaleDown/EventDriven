namespace UpscaleDown.EventDriven.Core.Query;

public sealed class Filters
{
    public List<Filter> Conditions { get; set; } = new List<Filter>();

    public static Filters Filter(Action<Filter> filter)
    {
        var f = new Filter();
        filter(f);
        var filters = new Filters();
        filters.Conditions.Add(f);
        return filters;
    }

    public Filters Or(Action<Filter> filter)
    {
        var f = new Filter();
        filter(f);
        Conditions.Add(f);
        return this;
    }
}